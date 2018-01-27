using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiButtonsAcademy : Academy {
	public int lessonNr = 0;

	public override void AcademyReset()
	{
		lessonNr = (int)resetParameters["lessonNr"];
	}

	public override void AcademyStep()
	{

	}
}
