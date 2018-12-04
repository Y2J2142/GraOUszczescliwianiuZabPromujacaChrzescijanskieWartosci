using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemListScript : MonoBehaviour {

	private event Action<ItemData> onItemClick;

	[SerializeField]
	private Text titleText;
	private GameObject itemButtonPrefab;
	private string title;
	private List<ItemData> items;



	// Use this for initialization
	void Start () {
		titleText = gameObject.transform.Find("Title").GetComponent<Text>();
		itemButtonPrefab = Resources.Load<GameObject>("Prefabs/ItemButton");
		titleText.text = title;
		var grid = gameObject.transform.Find("ItemPanel").GetComponent<GridLayoutGroup>();
		
		items.ForEach(item => 
		{
			var button = Instantiate(itemButtonPrefab, Vector3.zero, Quaternion.identity);
			button.transform.SetParent(grid.transform);
			var buttonImage = button.transform.GetComponent<Image>();
			button.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(() => OnItemClick(item, buttonImage));
			buttonImage.sprite = item.sprite;

		});
	}
	public void addAction(Action<ItemData> action)
	{
		onItemClick += action;
	}




	private void OnItemClick(ItemData data, Image buttonImage)
	{
		if(onItemClick != null)
			onItemClick(data);
	}


	public void SetProperties(List<ItemData> items, string title)
	{
		this.items = items;
		this.title = title;
	}



	// Update is called once per frame
	void Update () {
		
	}
}
