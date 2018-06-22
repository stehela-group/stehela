using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CText : CGameObject
{
	private TextMeshPro displayText;

	private static GameObject UICanvas = new GameObject();
	private GameObject text;
	public CText(string text)
	{
		this.text = new GameObject();

		if(! CText.UICanvas.GetComponent<Canvas>())
		{
			CText.UICanvas.AddComponent<Canvas>();
			CText.UICanvas.name = "Canvas";
		}

		this.displayText = this.text.AddComponent<TextMeshPro>();
		this.displayText.SetText(text);
		this.setName(text);

		this.text.transform.SetParent(CText.UICanvas.transform);
	}

	public void setText(string newText)
	{
		base.update();

		this.displayText.SetText(newText);
		this.setName(newText);
	}

	override public void update()
	{
		base.update();

		this.displayText.rectTransform.sizeDelta = new Vector2(this.getWidth(), this.getHeight());
	}

	override public void render()
	{
		base.render();

		Vector3 pos = new Vector3(getX(), getY() * -1, 0.0f);
		this.displayText.transform.position = pos;
	}

	override public void destroy()
	{
		base.destroy();

		Object.Destroy(this.displayText);
		Object.Destroy(this.text);
	}

	override public void setName(string aName)
	{
		base.setName("Texto - " + aName);
		this.text.name = "Texto - " + aName;
	}

	public void setColor(Color color)
	{
		this.displayText.color = color;
	}

	public void setFontSize(float size)
	{
		this.displayText.fontSize = size;
	}

	public void setCanAutoSize(bool canAutoSize)
	{
		this.displayText.autoSizeTextContainer = canAutoSize;
	}

	public void setAlignment(TextAlignmentOptions alignment)
	{
		this.displayText.alignment = alignment;
	}

	public void setVisible(bool visible)
	{
		this.displayText.enabled = visible;
	}

	public void setWrapping(bool wrapping)
	{
		this.displayText.enableWordWrapping = wrapping;
	}

	public static void setSortingLayerName(string aSortingLayerName)
	{
		CText.UICanvas.GetComponent<Canvas>().sortingLayerName = aSortingLayerName;
	}
}