using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(PoolManager))]
public class ObjectPoolerEditor : Editor {
	const string INFO = "풀링한 오브젝트에 다음을 적으세요 \nvoid OnDisable()\n{\n" +
		"    ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 \n" +
		"    CancelInvoke();    // Monobehaviour에 Invoke가 있다면 \n}";

	public override void OnInspectorGUI() {
		EditorGUILayout.HelpBox(INFO, MessageType.Info);
		base.OnInspectorGUI();
	}
}
#endif

public class PoolManager : MonoBehaviour {
	public static PoolManager inst;
	void Awake() => inst = this;
	//inst가 public이 아니기에 외부에서 접못함
	[Serializable]
	public class Pool {
		public string tag;
		public GameObject prefab;
		public int size;
	}


	//직렬화하는 클래스가 풀매니저 내부에 있기에 외부에서 접근못함

	[SerializeField] Pool[] pools; //정보가담긴것
	List<GameObject> spawnObjects; //모든게임오브젝트가 담긴것
	Dictionary<string, Queue<GameObject>> poolDictionary; //이름으로 queue로 저장하는 게임오브젝트하는 배열?그거만듬 배열에 배열인가?
	readonly string INFO = " 오브젝트에 다음을 적으세요 \nvoid OnDisable()\n{\n" +
		"    ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 \n" +
		"    CancelInvoke();    // Monobehaviour에 Invoke가 있다면 \n}";
	//const쓰면 에러나가지고 readonly써줌


	public static GameObject SpawnFromPool(string tag, Vector3 position) =>
		inst._SpawnFromPool(tag, position, Quaternion.identity);

	public static GameObject SpawnFromPool(string tag, Transform position) =>
	inst._SpawnFromPool(tag, position, Quaternion.identity);
	//static이라 쓸수있음 외부에서

	//오버로딩들
	public static GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) =>
		inst._SpawnFromPool(tag, position, rotation);

	public static T SpawnFromPool<T>(string tag, Vector3 position) where T : Component {
		GameObject obj = inst._SpawnFromPool(tag, position, Quaternion.identity);
		if (obj.TryGetComponent(out T component))
			return component;
		else {
			obj.SetActive(false);
			throw new Exception($"Component not found");
		}
	}

	public static T SpawnFromPool<T>(string tag, Vector3 position, Quaternion rotation) where T : Component {
		GameObject obj = inst._SpawnFromPool(tag, position, rotation);

		//out이 뭘까? component는?
		if (obj.TryGetComponent(out T component))
			return component;
		else {
			obj.SetActive(false);
			throw new Exception($"Component not found");
		}
	}

	public static List<GameObject> GetAllPools(string tag) {
		if (!inst.poolDictionary.ContainsKey(tag))
			throw new Exception($"Pool with tag {tag} doesn't exist.");

		return inst.spawnObjects.FindAll(x => x.name == tag); //비활성화된것까지 ,가져옴
	}

	public static List<T> GetAllPools<T>(string tag) where T : Component {
		List<GameObject> objects = GetAllPools(tag);

		if (!objects[0].TryGetComponent(out T component))
			throw new Exception("Component not found");

		return objects.ConvertAll(x => x.GetComponent<T>());
	}

	public static void ReturnToPool(GameObject obj) {
		if (!inst.poolDictionary.ContainsKey(obj.name))
			throw new Exception($"Pool with tag {obj.name} doesn't exist.");

		inst.poolDictionary[obj.name].Enqueue(obj); //Queue게임오브젝트가 할당
	}

	[ContextMenu("GetSpawnObjectsInfo")]
	void GetSpawnObjectsInfo() {
		foreach (var pool in pools) {
			int count = spawnObjects.FindAll(x => x.name == pool.tag).Count;
			Debug.Log($"{pool.tag} count : {count}");
		}
	}

	GameObject _SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
		if (!poolDictionary.ContainsKey(tag))
			throw new Exception($"Pool with tag {tag} doesn't exist."); //예외를 던져주면 그다음의 것은 실행안함
		//풀딕셔너리에 이름이 있다는 것은 프리팹과 갯수가 있는거니까

		//독립성을 위해서 변수하나 만듬
		// 큐에 없으면 새로 추가
		Queue<GameObject> poolQueue = poolDictionary[tag];
		
		if (poolQueue.Count <= 0) {
			//pools에 이름이 같은것을 
			Pool pool = Array.Find(pools, x => x.tag == tag); 
	
			var obj = CreateNewObject(pool.tag, pool.prefab);
			ArrangePool(obj); //여기에 추가하는게 있으니 처음생성할때도 해줌
		}


		// 큐에서 꺼내서 사용
		GameObject objectToSpawn = poolQueue.Dequeue();
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		//활성화전에 위치와 회전값을 잡아줌
		objectToSpawn.SetActive(true);

		return objectToSpawn;
	}

	GameObject _SpawnFromPool(string tag, Transform position, Quaternion rotation) {
		if (!poolDictionary.ContainsKey(tag))
			throw new Exception($"Pool with tag {tag} doesn't exist."); //예외를 던져주면 그다음의 것은 실행안함
																		//풀딕셔너리에 이름이 있다는 것은 프리팹과 갯수가 있는거니까

		//독립성을 위해서 변수하나 만듬
		// 큐에 없으면 새로 추가
		Queue<GameObject> poolQueue = poolDictionary[tag];

		if (poolQueue.Count <= 0) {
			//pools에 이름이 같은것을 
			Pool pool = Array.Find(pools, x => x.tag == tag);

			var obj = CreateNewObject(pool.tag, pool.prefab);
			ArrangePool(obj); //여기에 추가하는게 있으니 처음생성할때도 해줌
		}


		// 큐에서 꺼내서 사용
		GameObject objectToSpawn = poolQueue.Dequeue();
		objectToSpawn.transform.position = position.position;
		objectToSpawn.transform.rotation = rotation;

		//활성화전에 위치와 회전값을 잡아줌
		objectToSpawn.SetActive(true);

		return objectToSpawn;
	}

	public bool isStart = false;
	void Start() {
		spawnObjects = new List<GameObject>();
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		// 미리 생성
		foreach (Pool pool in pools) {

			//pools클래스를 하나씩 파헤쳐봄
			//게임오브젝트를 생성하자마자 할당이 됨 new Queue가
			poolDictionary.Add(pool.tag, new Queue<GameObject>());
			for (int i = 0; i < pool.size; i++) {
				//리턴투 풀로 Queue에 할당이됨
				var obj = CreateNewObject(pool.tag, pool.prefab);
				ArrangePool(obj);
			}

			// OnDisable에 ReturnToPool 구현여부와 중복구현 검사
			if (poolDictionary[pool.tag].Count <= 0)
				Debug.LogError($"{pool.tag}{INFO}");
			else if (poolDictionary[pool.tag].Count != pool.size)
				Debug.LogError($"{pool.tag}에 ReturnToPool이 중복됩니다");
		}

		isStart = true;
	}

	GameObject CreateNewObject(string tag, GameObject prefab) {
		var obj = Instantiate(prefab, transform);
		obj.name = tag;
		obj.SetActive(false); // 비활성화시 ReturnToPool을 하므로 Enqueue가 됨
		return obj;
	}

	void ArrangePool(GameObject obj) {
		// 추가된 오브젝트 묶어서 정렬
		bool isFind = false;
		for (int i = 0; i < transform.childCount; i++) {
			if (i == transform.childCount - 1) {
				//마지막일떄 
				obj.transform.SetSiblingIndex(i);
				spawnObjects.Insert(i, obj); //리스트에 몇번째에다가 게임오브젝트추가함
				break;
			} else if (transform.GetChild(i).name == obj.name)
				isFind = true;
			else if (isFind) {
				obj.transform.SetSiblingIndex(i);
				spawnObjects.Insert(i, obj);
				break;
			}
		}
	}
}