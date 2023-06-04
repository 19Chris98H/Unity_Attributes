# Attributes for Unity
 
This package currently provides four attributes and may be extended in the future.

You can simply download the single [package file](Attributes.unitypackage) and drop it into your project to import everything you need without cloning the whole repository. 

## Dropdown List Attribute
As can be seen in the figure, dropdown lists can be used in the inspector with this attribute. Unlike what Unity provides out of the box, this is not a static list defined by enums, but a dynamic list that is customizable outside the code at runtime of the editor. Thus, it is possible to reference entries of a Json file, or the elements of a list defined in the inspector even of another GameObject. The attribute can be used in the following ways:

```C#
public class ItemShop : MonoBehaviour
{
    public List<string> items;
    
    [DropdownList("Items", "items")]
    public string specialOffer;
}
```

The first parameter is the label for the dropdown list. The second parameter is the name of the list within the same class.
However, lists from other classes can also be used. There must exist a reference to the class whose name is now passed as the second parameter and the name of the list within the external class which is passed as the third parameter.

```C#
public class ItemManager : MonoBehaviour
{
    public List<string> items;
}

public class ItemShop : MonoBehaviour
{
    public ItemManager itemManager;
    
    [DropdownList("Items", "itemManager", "items")]
    public string specialOffer;
}
```

Instead of saving the actual entry, you can also save only the index of the entry. However, this should only be used with caution, as the indexes are not synchronized with any changes to the list. The use of indexes then looks as follows:

```C#
public class ItemManager : MonoBehaviour
{
    public List<string> items;
}

public class ItemShop : MonoBehaviour
{
    public ItemManager itemManager;
    
    [DropdownList("Items", "itemManager", "items", true)]
    public int specialOffer;
}
```

If you want to create the list at the runtime of the editor, for example by reading the data from a Json file, you have to use the ```EditorData``` class. In this class you can create fields for the desired lists, read the data from the Json files and save them into the lists. If you have configured everything there to load the data, you can reference the list as follows:

```C#
public class EditorData
{
    public static EditorData Instance => _instance ?? (_instance = new EditorData());
    static EditorData _instance;

    public List<string> quests;

    EditorData()
    {
        ReloadData();
    }
    
    //Here I used the JsonHelper class from one of my other repositories to load the needed data, but you can use here whatever you want.
    static void LoadQuestData(ref List<string> destination)
    {
        var definitions = new Dictionary<string, QuestDefinition>();
        JsonHelper.LoadJson(ref definitions, "Assets/Resources/Definitions/QuestDefinitions.json");
        destination = definitions.Select(entry => entry.Name).ToList();
    }

    public void ReloadData()
    {
        LoadQuestData(ref quests);
    }
}

public class Quest : MonoBehaviour
{
    [DropdownList("Quest", "quests", true)]
    public string quest;
}
```

If you are interested you can find the class I used to read the json files in my repository [Unity_Json_Data_Manager](https://github.com/19Chris98H/Unity_Json_Data_Manager).

## Hide If Attribute
Apart from a few minor adjustments, this attribute is largely taken over from [Or-Aviram](https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/). It can be used to define conditions under which a field is either no longer visible in the inspector, or is no longer editable and is available only as read only. This can be used, for example, to display additional options when a toggle is set in the inspector and to hide them again when the toggle is deselected, as can be seen in the illustrations. The attribute is used as follows:

```C#
public enum GizmoShapes
{
    Sphere,
    Ray
}

public class GizmoRenderer : MonoBehaviour
{
    public GizmoShapes Shape;

    [HideIf("Shape", GizmoShapes.Sphere, ComparisonType.NotEqual, DisablingType.Hide)]
    public float Radius = 1;

    [HideIf("Shape", GizmoShapes.Ray, ComparisonType.NotEqual, DisablingType.Hide)]
    public Vector3 Direction = Vector3.one;
}
```

The comparisons that can be performed are defined in the ComparisonType enum and are:

```C#
ComparisonType.Equals
ComparisonType.NotEqual
ComparisonType.GreaterThan
ComparisonType.SmallerThan
ComparisonType.SmallerOrEqual
ComparisonType.GreaterOrEqual
```

The following data types can be used:

```C#
Type.Byte
Type.SByte
Type.UInt16
Type.UInt32
Type.UInt64
Type.Int16
Type.Int32
Type.Int64
Type.Decimal
Type.Double
Type.Single
```

Since integers are comparable, enums can also be used.

## Read Only Attribute
This attribute shows a field as read only in the inspector. This is useful when setting specific values via editor scripts. The attribute can be used as follows:

```C#
public class Checkpoint : MonoBehaviour
{
    [ReadOnly] public int checkpointID;
}
```

## Read Only Runtime Attribute
This attribute shows a field as read only in the inspector and only when the editor is in play mode. This allows to keep the inspector as manageable as possible while editing, yet to analyze the behavior of certain values at runtime. The attribute is used as follows:

```C#
public class DataManager : MonoBehaviour
{
    [ReadOnlyRuntime] public int highscore;
}
```

## Contact
As a developer, you never stop learning. Therefore, I am always open to constructive criticism and suggestions for improvement. Feel free to contact me via [LinkedIn](https://www.linkedin.com/in/christian-h%C3%B6rath-0ba068201/) or by [email](mailto:hoerath.christian@gmail.com).
