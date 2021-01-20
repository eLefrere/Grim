using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Author : Sam Hemming
/// 
/// Puzzle discription:
///		Reguires the player to place parts(Gears) in place in correct order.
///		Parts can not be taken out once placed and there is no penalty in trying to place them incorrectly.
///		
/// List all parts in order and give first not completed isUpNext tag.
/// Parts trigger event on complete that signals puzzle to move tag one place forward.
/// </summary>
public class ClockPuzzle : Puzzle
{
	private List<ClockGear> gears = new List<ClockGear>();
	private readonly string clockPuzzleGearCompleteCode = "ClockPuzzlePartComplete";
	

	/// <summary>
	/// Sub to parts complete event and prime the parts into list with error checking.
	/// </summary>
	private void Awake()
	{
		EventManager.onPuzzlepartComplete += EventCodeCheck;

		foreach (Puzzlepart part in puzzleParts)
		{
			ClockGear gear = part.GetComponent<ClockGear>();

			if (gear != null)
			{
				gears.Add(gear);
				gear.isUpNext = false;
				gear.inPosition = false;
			}
			else if (DebugTable.PuzzleDebug)
				Debug.LogError($"{this.name}: Invalid PuzzlePart! {part.name}: Does not contain ClockGear!");
		}

		CheckGearProgress();
	}

	/// <summary>
	/// Makes sure that check is run only on correct event code.
	/// </summary>
	/// <param name="eventCode">Should be code from completing clock puzzle part.</param>
	private void EventCodeCheck(string eventCode)
	{
		if (eventCode != clockPuzzleGearCompleteCode)
			return;

		CheckGearProgress();
	}

	/// <summary>
	/// Checks parts progress and gives turn to first uncompleted part.
	/// </summary>
	private void CheckGearProgress()
	{
		bool nextFound = false;

		for (int i = 0; i < gears.Count; ++i)
		{
			if (gears[i].inPosition || nextFound)
			{
				gears[i].isUpNext = false;
			}
			else
			{
				gears[i].isUpNext = true;
				nextFound = true;
			}
		}
	}
}
