using UnityEngine;
using UnityEngine.UI;
public class CText : CGameObject
{
	private Text displayText;
	private GameObject text;
	public CText(string text)
	{
		this.text = new GameObject();
		this.displayText = this.text.AddComponent<Text>();
		this.displayText.text = text;
		this.setName("Texto - " + text);
	}

	public void setText(string newText)
	{
		base.update();

		this.displayText.text = newText;
	}

	override public void render()
	{
		base.render();

		Vector3 pos = new Vector3(getX(), getY() * -1, 0.0f);
		this.displayText.transform.position = pos;
	}
}