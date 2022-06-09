using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An item that can be collected.
/// It contains the behaviour to "float" and
/// its name in English and Spanish
/// </summary>
public class CollectableItem : MonoBehaviour,
    System.IEquatable<CollectableItem>
{

    // The name

    [Tooltip("The name of this item in english")]
    public string EnglishName;
    [Tooltip("The name of this item in spanish")]
    public string SpanishName;
    [Tooltip("The name of this item in portuguese")]
    public string PortugueseName;

    [Tooltip("The english pronuntiation")]
    [SerializeField]
    private AudioClip enClip;

    [Tooltip("The spanish pronuntiation")]
    [SerializeField]
    private AudioClip esClip;

    [Tooltip("The audio in the third language")]
    [SerializeField]
    private AudioClip prClip;


    // The name in the selected lang mode
    // The lang mode is an integer: 0 for eng and 1 for sp
    internal static LangMode langMode = LangMode.Spanish;

    /// <summary>
    /// Get the name of this collectable item based on the selected
    /// language, which will hopefully be user adjustable.
    /// </summary>
    public string LangDependantName 
        => chooseBasedOnLang(SpanishName, EnglishName, PortugueseName);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Spin the object around the target at 90 degrees/second.
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
    }

    /// <inheritdoc/>
    public override string ToString() => 
        $"[ES: {SpanishName}. EN: {EnglishName}. PR: {PortugueseName}";

    /// <inheritdoc/>
    public bool Equals(CollectableItem other) =>
        this.EnglishName == other.EnglishName ||
        this.SpanishName == other.SpanishName ||
        this.PortugueseName == other.PortugueseName;

    internal AudioClip Pronuntiation => chooseBasedOnLang(esClip, enClip, prClip);

    private static T chooseBasedOnLang<T>(T optionES, T optionEN, T optionPR)
    {
        switch (langMode)
        {
            case LangMode.Spanish: return optionES;
            case LangMode.English: return optionEN;
            case LangMode.Portuguese: return optionPR;
            default: return optionEN;
        }
    }

    
}

/// <summary>
/// The language mode for the game.
/// </summary>
internal enum LangMode
{
    Spanish,
    English,
    // A desperate move
    Portuguese,
}