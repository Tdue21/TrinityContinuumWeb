namespace TrinityContinuum.TestData;

public static class CharacterData
{ 
	public static string OneJson => """
	{
		"id": 1,
		"name": "Connor McCormick",
		"player": "Thomas",
		"concept": "Private Investigator",
	    "originPath": { "name": "", "dots": 1 },
	    "rolePath": { "name": "", "dots": 1 },
	    "societyPath": { "name": "", "dots": 1 }
	}
	""";

	public static string TwoJson => """
	{
		"id": 2,
		"name": "Jason Voorhees",
		"player": "Ken",
		"concept": "Psychotic Killer",
	    "originPath": { "name": "", "dots": 1 },
	    "rolePath": { "name": "", "dots": 1 },
	    "societyPath": { "name": "", "dots": 1 }
	}
	""";

	public static string ThreeJson => """
	{
		"id": 3,
		"name": "Freddy Krueger",
		"player": "Robert",
		"concept": "Malevolent Spirit",
	    "originPath": { "name": "", "dots": 1 },
	    "rolePath": { "name": "", "dots": 1 },
	    "societyPath": { "name": "", "dots": 1 }
	}
	""";
}