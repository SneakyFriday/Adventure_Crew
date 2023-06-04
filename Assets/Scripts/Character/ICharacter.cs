public interface ICharacter
{
    public void ChangeAttentionValue(int value);
    public SO_Item[] GetDesiredObjects();
    public SO_Character.CharacterClass GetCharacterClass();
}