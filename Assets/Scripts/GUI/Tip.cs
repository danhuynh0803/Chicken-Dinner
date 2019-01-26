using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tip : MonoBehaviour
{

    public Text tipText;

    void Start()
    {
        float rate = Random.Range(0.0f, 14.0f);

        if (rate > 13.0f)
        {
            tipText.text = "Tip 12: You lose Morale after flicking Goblins and Orcs.";
        }
        if (rate > 12.0f)
        {
            tipText.text = "Tip 13: This Tip is cursed. Please play another level to be uncursed.";
        }
        if (rate > 11.0f)
        {
            tipText.text = "Tip 12: About midway through the bridge, the War Wagon slows down.";
        }
        if (rate > 10.0f)
        {
            tipText.text = "Tip 11: Green means speed boost. Blue means shielded. Margenta means both.";
        }
        if (rate > 9.0f)
        {
            tipText.text = "Tip 1: Green Wizards give a lane-wide speed boost.";
        }
        else if (rate > 8.0f)
        {
            tipText.text = "Tip 2: Princesses will peek before making a dash for the other side.";
        }
        else if (rate > 7.0f)
        {
            tipText.text = "Tip 3: Blue Wizards shield allies.";
        }
        else if (rate > 6.0f)
        {
            tipText.text = "Tip 4: Abilities can be equipped/changed in the Store.";
        }
        else if (rate > 5.0f)
        {
            tipText.text = "Tip 5: Use livestock to recover Morale.";
        }
        else if (rate > 4.0f)
        {
            tipText.text = "Tip 6: Miners drop dynamite when flicked. Dynamite detonate after a second.";
        }
        else if (rate > 3.0f)
        {
            tipText.text = "Tip 7: After losing their armor, Knights move a bit faster.";
        }
        else if (rate > 2.0f)
        {
            tipText.text = "Tip 8: The in-game achievements for the Orc and Gem levels reward Gold.";
        }
        else if (rate > 1.0f)
        {
            tipText.text = "Tip 9: Flicks only send villagers to take a bath in the river.";
        }
        else
        {
            tipText.text = "Tip 10: Please follow us <TBA> on Twitter.";
        }
    }
}