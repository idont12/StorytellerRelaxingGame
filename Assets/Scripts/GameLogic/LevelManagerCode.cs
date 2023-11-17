using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static CharacterStatus;
using UnityEngine.SceneManagement;

public class LevelManagerCode : MonoBehaviour
{
    [SerializeField] int LevelNum;

    public bool canPlay = true;

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level"+(LevelNum+1).ToString());
    }
    public void GoToStart()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    [SerializeField] List<DropSlot> SliderOrder = new List<DropSlot>();
    [SerializeField] List<ItemCanDrag> allCharacters = new List<ItemCanDrag>();

    [SerializeField] List<MainCharcter> CharactersStatusTraker = new List<MainCharcter>();

    [SerializeField] List<Condition> Defult = new List<Condition>();
    [SerializeField] List<Condition> conditions = new List<Condition>();
    [SerializeField] Animator IconFinalLevel;
    [SerializeField] GameEvent FinalLevelSound;
    [SerializeField] GameObject PopNextLevel;
    //[SerializeField] ItemInfo ItemInfo;
    bool isFewFirstCh(string word, int characterNum, string eqealTo)
    {
        return word.ToString().Substring(0, characterNum) == eqealTo;
    }

    private void Start()
    {
        ItemInfo TestItem = ScriptableObject.CreateInstance<ItemInfo>();
        foreach (Condition condition in Defult)
        {
            //if (condition.isSituation)
            //{
            //    foreach (ObjectName thisObject in condition.SituationElement)
            //    {
            //        int objectId = TestItem.ObjectToID(thisObject);
            //        if (isFewFirstCh(thisObject.ToString(), 2, "Bg"))
            //        {
            //            if (condition.BackgroundsIDs.Contains(objectId) == false)
            //            {
            //                condition.BackgroundsIDs.Add(objectId);
            //            }
            //        }
            //        else if (isFewFirstCh(thisObject.ToString(), 2, "Ch"))
            //        {
            //            if (condition.CharactersIDs.Contains(objectId) == false)
            //            {
            //                condition.CharactersIDs.Add(objectId);
            //            }
            //        }
            //    }
            //}
            //if (condition.isCharacterStatus)
            //{
            //    foreach (CharacterStatus state in condition.conditionsStatus)
            //    {
            //        int MainID = TestItem.ObjectToID(state.mainObject);
            //        if (state.mainCharacteID == 0)
            //        {
            //            state.mainCharacteID = MainID;
            //        }
            //        int SecenderyID = TestItem.ObjectToID(state.secondaryObject);
            //        if (state.secondaryCharacterID == 0)
            //        {
            //            state.secondaryCharacterID = SecenderyID;
            //        }
            //    }
            //}
            if (condition.willChangeAnimation)
            {
                foreach (ChangeAnimation Animation in condition.ChangeAnimations)
                {
                    int animationId = TestItem.ObjectToID(Animation.Character);
                    if (isFewFirstCh(Animation.Character.ToString(), 2, "Ch") && Animation.CharacterID == 0)
                    {
                        Animation.CharacterID = animationId;
                    }
                }
            }
            if (condition.willChangeCharacterStatus)
            {
                foreach (CharacterStatus state in condition.Change_characterFirendStatus)
                {
                    int MainID = TestItem.ObjectToID(state.mainObject);
                    if (state.mainCharacteID == 0)
                    {
                        state.mainCharacteID = MainID;
                    }
                    int SecenderyID = TestItem.ObjectToID(state.secondaryObject);
                    if (state.secondaryCharacterID == 0)
                    {
                        state.secondaryCharacterID = SecenderyID;
                    }
                }
            }

        }
        foreach (Condition condition in conditions)
        {
            if (condition.isSituation)
            {
                foreach (ObjectName thisObject in condition.SituationElement)
                {
                    int objectId = TestItem.ObjectToID(thisObject);
                    if (isFewFirstCh(thisObject.ToString(), 2, "Bg"))
                    {
                        if (condition.BackgroundsIDs.Contains(objectId) == false)
                        {
                            condition.BackgroundsIDs.Add(objectId);
                        }
                    }
                    else if (isFewFirstCh(thisObject.ToString(), 2, "Ch"))
                    {
                        if (condition.CharactersIDs.Contains(objectId) == false)
                        {
                            condition.CharactersIDs.Add(objectId);
                        }
                    }
                }
            }
            if (condition.isCharacterStatus)
            {
                foreach (CharacterStatus state in condition.conditionsStatus)
                {
                    int MainID = TestItem.ObjectToID(state.mainObject);
                    if (state.mainCharacteID == 0)
                    {
                        state.mainCharacteID = MainID;
                    }
                    int SecenderyID = TestItem.ObjectToID(state.secondaryObject);
                    if (state.secondaryCharacterID == 0)
                    {
                        state.secondaryCharacterID = SecenderyID;
                    }
                }
            }
            if (condition.willChangeAnimation)
            {
                foreach (ChangeAnimation Animation in condition.ChangeAnimations)
                {
                    int animationId = TestItem.ObjectToID(Animation.Character);
                    if (isFewFirstCh(Animation.Character.ToString(), 2, "Ch") && Animation.CharacterID==0)
                    {
                        Animation.CharacterID = animationId;
                    }
                }
            }
            if (condition.willChangeCharacterStatus)
            {
                foreach (CharacterStatus state in condition.Change_characterFirendStatus)
                {
                    int MainID = TestItem.ObjectToID(state.mainObject);
                    if (state.mainCharacteID == 0)
                    {
                        state.mainCharacteID = MainID;
                    }
                    int SecenderyID = TestItem.ObjectToID(state.secondaryObject);
                    if (state.secondaryCharacterID == 0)
                    {
                        state.secondaryCharacterID = SecenderyID;
                    }
                }
            }
            //if (condition.)
            //{

            //}
            
        }
        for(int i=0; i< SliderOrder.Count;i++)
        {
            SliderOrder[i].SlotPlace = i;
        }
    }

    IEnumerator WaitToStartSotory()
    {
        yield return new WaitForSeconds(0.3f);
        ChackStory();
    }

    public void ChackStoryInDelay()
    {
        print("Wait");
        StartCoroutine(WaitToStartSotory());
    }

    //public void ChackStory()
    //{
    //    print("Nothing");
    //}
    public void ChackStory()
    {
        //EditorApplication.isPaused = true;
        //List<MainCharcter> CharactersStatusTraker = new List<MainCharcter>();
        CharactersStatusTraker = new List<MainCharcter>();

        foreach (ItemCanDrag character in allCharacters)
        {
            MainCharcter newCharacter = new MainCharcter();
            newCharacter.ID = character.objectID;
            CharactersStatusTraker.Add(newCharacter);
        }

        print("Start");
        
        foreach (DropSlot slide in SliderOrder)
        {
            if (slide.backgroundManager!=null)
            {
                //איסוף נותונים יבשים על כל האובייקטים בסלייד
                int BackgroundId = slide.ObjectSlotID;
                List<int> ChractersIDs = new List<int>();
                List<GameObject> ChractersObjects = new List<GameObject>();
                print(slide.backgroundManager.objectInSlotList.Count);

                int FinishLevelGoal = 0;
                int ProgressFinishLevel = 0;
                bool FinishChack = false;

                //foreach (GameObject thisObject in slide.backgroundManager.objectInSlotList)
                //{
                //    if (thisObject != null)
                //    {
                //        ChractersIDs.Add(thisObject.GetComponent<ItemCanDrag>().objectID);
                //        ChractersObjects.Add(thisObject);
                //        ChractersColliderID.
                //    }
                //}
                print("slide.backgroundManager.objectInSlotList.Count" + slide.backgroundManager.objectInSlotList.Count.ToString());
                string ids = "";
                for (int i =0; i< slide.backgroundManager.objectInSlotList.Count;i++)
                {
                    print("EndStartLoop");
                    GameObject thisObject = slide.backgroundManager.objectInSlotList[i];
                    if (thisObject != null)
                    {
                        ChractersIDs.Add(thisObject.GetComponent<ItemCanDrag>().objectID);
                        ChractersObjects.Add(thisObject);
                        ids += thisObject.GetComponent<ItemCanDrag>().objectID.ToString() + ", ";
                    }
                }
                print("all ids" + ids);
                print("EndLoop");
                foreach (Condition condition in Defult)
                {
                    //Effect
                    if (condition.willChangeAnimation)
                    {
                        print("ChangeAnimation defolt");
                        condition.ApplyChangeAnimation(ChractersIDs, ChractersObjects);
                    }
                    if (condition.willChangeCharacterStatus)
                    {
                        print("EnterApplyStatus");
                        CharactersStatusTraker = condition.ApplyChangeStatus(CharactersStatusTraker);
                    }
                }

                foreach (Condition condition in conditions)
                {
                    if (condition.WillHelpFinishLevel)
                    {
                        FinishLevelGoal++;
                    }
                    print("FinishLevelGoal: "+FinishLevelGoal.ToString());
                }

                    //בדיקת סיטואציה
                    foreach (Condition condition in conditions)
                {
                    print(condition.conditionName);
                    bool ChackAllWhen = true;
                    //Couse
                    if (condition.isSituation && ChackAllWhen)
                    {
                        print("SituationRetuen" + condition.ChackSituation(BackgroundId, ChractersIDs).ToString());
                        ChackAllWhen = condition.ChackSituation(BackgroundId, ChractersIDs);
                    }

                    if (condition.isCharacterStatus && ChackAllWhen)
                    {
                        print("GotCondition");
                        print(condition.ChackCharacterStatus(ChractersIDs, CharactersStatusTraker));
                        ChackAllWhen = condition.ChackCharacterStatus(ChractersIDs, CharactersStatusTraker);
                    }
                    print("ChackAllWhen:" + ChackAllWhen.ToString());
                    //Effect
                    if (ChackAllWhen)
                    {
                        if (condition.willChangeAnimation)
                        {
                            condition.ApplyChangeAnimation(ChractersIDs, ChractersObjects);
                        }
                        if (condition.willChangeCharacterStatus)
                        {
                            print("EnterApplyStatus");
                            CharactersStatusTraker =  condition.ApplyChangeStatus(CharactersStatusTraker);
                        }
                        if (condition.willCallEvent)
                        {
                            condition.Event.Raise();
                        }
                        if (condition.WillHelpFinishLevel)
                        {
                            print("FinishLevelnow: " + ProgressFinishLevel.ToString() + " Goal: " + FinishLevelGoal.ToString());
                            ProgressFinishLevel++;
                            if (ProgressFinishLevel == FinishLevelGoal)
                            {
                                FinishChack = true;
                                FinishLevel();
                            }
                        }
                    }
                    if (IconFinalLevel.GetBool("Solve") && FinishChack==false)
                    {
                        UnfinishLevel();
                    }
                }
            }
        }
    }
    
    public void FinishLevel()
    {
        print("FinishLevel");
        canPlay = false;
        PlayerPrefs.SetInt("Level" + LevelNum.ToString(),1);
        FinalLevelSound.Raise();
        if (IconFinalLevel!=null)
        {
            IconFinalLevel.SetBool("Solve", true);
        }
        if (PopNextLevel!=null)
        {
            PopNextLevel.SetActive(true);
            LeanTween.scale(PopNextLevel, new Vector3(1, 1, 1), 0.2f);
        }
    }

    public void UnfinishLevel()
    {
        print("unfinishLevel");
        canPlay = true;
        if (IconFinalLevel != null)
        {
            IconFinalLevel.SetBool("Solve", false);
        }
    }

    [System.Serializable]
    public class Condition
    {
        public string conditionName;
        public int ID;

        int GetCharacterFromMainList(int CharacterId, List<MainCharcter> CharacterList)
        {
            for (int i = 0; i < CharacterList.Count; i++)
            {
                if (CharacterList[i].ID == CharacterId)
                {
                    return i;
                }
            }
            return -1;
        }

        int GetRelevantStatePlace(List<CharacterStatus> characterStatuses, CharacterStatus newState)
        {
            for (int i=0;i< characterStatuses.Count; i++)
            {
                bool needSecenderyCharacter = true;
                if (newState.relationshipType == RelationshipType.Friendship)
                {
                    needSecenderyCharacter = characterStatuses[i].secondaryCharacterID == newState.secondaryCharacterID; ;
                }
                if (needSecenderyCharacter && characterStatuses[i].relationshipType == newState.relationshipType)
                {
                    return i;
                }
            }
            return -1;
        }

       

        [Header("---Couse---")]
        [Header("Chack Situation")]
        public bool isSituation;
        public List<ObjectName> SituationElement = new List<ObjectName>();
        public List<int> BackgroundsIDs = new List<int>();
        public bool isCharacterAddition = false;
        public List<int> CharactersIDs = new List<int>();
        public bool useSpacificSlot = false;
        //public List<int> Cha = new List<int>();

        public bool ChackSituation(int BackgroundID, List<int> CharacterID)
        {
            print(CharacterID.Count.ToString() + "_" + CharactersIDs.Count.ToString());
            if ((BackgroundsIDs.Count > 0 && BackgroundsIDs.Contains(BackgroundID) || BackgroundsIDs.Count == 0) && (CharacterID.Count == CharactersIDs.Count || isCharacterAddition))
            {
                print("Finish first if");
                for(int i=0;i< CharactersIDs.Count; i++)
                {
                    int id = CharactersIDs[i];
                    print("id"+ id);
                    print("get to secend if:" + (CharacterID.Contains(id)).ToString());

                    bool Contain = false;
                    if (useSpacificSlot)
                    {
                        Contain = CharacterID[i] == id;
                    }
                    else
                    {
                        print("not spacific");
                        Contain = CharacterID.Contains(id);
                    }
                    if (Contain == false)
                    {
                        print("ReturnFalse");
                        return false;
                    }

                }

                return true;
            }
            return false;
        }
        [Header("Chack Status")]
        public bool isCharacterStatus;
        public List<CharacterStatus> conditionsStatus = new List<CharacterStatus>();
        public bool ChackCharacterStatus(List<int> charactersIDS,List<MainCharcter> CharactersStatusTraker)
        {
            foreach (CharacterStatus conditionStatus in conditionsStatus)
            {
                int characterInList = GetCharacterFromMainList(conditionStatus.mainCharacteID, CharactersStatusTraker);
                print("characterInList: " + characterInList.ToString());
                print("GetRelevantStatePlace(CharactersStatusTraker[characterInList].mySituations, conditionStatus): " + GetRelevantStatePlace(CharactersStatusTraker[characterInList].mySituations, conditionStatus).ToString());
                if(characterInList != -1  && GetRelevantStatePlace(CharactersStatusTraker[characterInList].mySituations, conditionStatus) != -1)
                {
                    print("if result: " + (CharactersStatusTraker[characterInList].mySituations[GetRelevantStatePlace(CharactersStatusTraker[characterInList].mySituations, conditionStatus)].isHappand != conditionStatus.isHappand).ToString());

                    if (CharactersStatusTraker[characterInList].mySituations[GetRelevantStatePlace(CharactersStatusTraker[characterInList].mySituations, conditionStatus)].isHappand != conditionStatus.isHappand)
                    {
                        return false;
                    }
                    
                    //foreach (CharacterStatus status in CharactersStatusTraker[characterInList].mySituations)
                    //{
                    //    print("ChackIFS: " + (conditionStatus.secondaryCharacterID == status.secondaryCharacterID).ToString() + "_" + (conditionStatus.isHappand == status.isHappand).ToString() + "_" + (conditionStatus.relationshipType == status.relationshipType).ToString());
                    //    if ((conditionStatus.secondaryCharacterID == status.secondaryCharacterID || conditionStatus.relationshipType != RelationshipType.Friendship) && conditionStatus.isHappand == status.isHappand && conditionStatus.relationshipType == status.relationshipType)
                    //    {
                    //        break;
                    //    }
                    //    return false;
                    //}



                    //if (conditionStatus.relationshipType == RelationshipType.Friendship)
                    //{
                    //    foreach (CharacterStatus status in CharactersStatusTraker[characterInList].mySituations)
                    //    {
                    //        print("ChackIFS: " + (conditionStatus.secondaryCharacterID == status.secondaryCharacterID).ToString() + "_" + (conditionStatus.isHappand == status.isHappand).ToString() + "_" + (conditionStatus.relationshipType == status.relationshipType).ToString());
                    //        if(conditionStatus.secondaryCharacterID == status.secondaryCharacterID && conditionStatus.isHappand == status.isHappand && conditionStatus.relationshipType == status.relationshipType)
                    //        {
                    //            break;
                    //        }
                    //        return false;
                    //    }
                    //}
                    //else if (conditionStatus.relationshipType == RelationshipType.NeedHelp || conditionStatus.relationshipType == RelationshipType.Scared)
                    //{
                    //    foreach (CharacterStatus status in CharactersStatusTraker[characterInList].mySituations)
                    //    {
                    //        if (conditionStatus.isHappand == status.isHappand && conditionStatus.relationshipType == status.relationshipType)
                    //        {
                    //            break;
                    //        }
                    //        return false;
                    //    }
                    //}
                }
                else
                {
                    print("isHappand: " + (conditionStatus.isHappand).ToString());
                    if (conditionStatus.isHappand)
                    {
                        return false;
                    }
                   
                    
                }
            }
            return true;
        }


        [Header("---Effect---")]
        [Header("Change Animation")]
        public bool willChangeAnimation;

        public List<ChangeAnimation> ChangeAnimations = new List<ChangeAnimation>();
        public void ApplyChangeAnimation(List<int> charactersIDS, List<GameObject> charactersObjects)
        {
            foreach (ChangeAnimation changeAnimation in ChangeAnimations)
            {
                int characterIndex = charactersIDS.IndexOf(changeAnimation.CharacterID);
                print("characterIndexAnimation: " + characterIndex.ToString());
                if (characterIndex != -1)
                {
                    charactersObjects[characterIndex].GetComponent<Animator>().SetInteger(changeAnimation.AnimationName, changeAnimation.AnimationNum);
                }
                else
                {
                    print("Error in ApplyChangeAnimation!");
                }
            }

        }
        
        [Header("Change Characater Status")]
        public bool willChangeCharacterStatus;
        public List<CharacterStatus> Change_characterFirendStatus = new List<CharacterStatus>();

        public List<MainCharcter> ApplyChangeStatus(List<MainCharcter> allCharcters)
        {
            foreach (CharacterStatus newStatus in Change_characterFirendStatus)
            {
                print("newStatus: " + newStatus.relationshipType.ToString() + "_" + newStatus.mainCharacteID.ToString() + "_" + newStatus.isHappand);
                int characterPlaceInList = GetCharacterFromMainList(newStatus.mainCharacteID, allCharcters);
                if (characterPlaceInList != -1)
                {

                    if (newStatus.relationshipType == RelationshipType.Friendship)
                    {
                        int releventStatePlace = GetRelevantStatePlace(allCharcters[characterPlaceInList].mySituations, newStatus);
                        print("releventStatePlace1" + releventStatePlace.ToString());
                        if (releventStatePlace == -1)
                        {
                            CharacterStatus newLine = new CharacterStatus();
                            newLine.mainCharacteID = newStatus.mainCharacteID;
                            newLine.secondaryCharacterID = newStatus.secondaryCharacterID;
                            newLine.isHappand = newStatus.isHappand;
                            allCharcters[characterPlaceInList].mySituations.Add(newLine);
                        }
                        else
                        {
                            allCharcters[characterPlaceInList].mySituations[releventStatePlace].isHappand = newStatus.isHappand;
                        }
                    }
                    else if (newStatus.relationshipType == RelationshipType.NeedHelp || newStatus.relationshipType == RelationshipType.Scared || newStatus.relationshipType == RelationshipType.situation1 || newStatus.relationshipType == RelationshipType.situation2 || newStatus.relationshipType == RelationshipType.situation3)
                    {
                        int releventStatePlace = GetRelevantStatePlace(allCharcters[characterPlaceInList].mySituations, newStatus);
                        print("releventStatePlace2" + releventStatePlace.ToString());
                        if (releventStatePlace == -1)
                        {
                            CharacterStatus newLine = new CharacterStatus();
                            newLine.relationshipType = newStatus.relationshipType;
                            newLine.mainCharacteID = newStatus.mainCharacteID;
                            newLine.isHappand = newStatus.isHappand;
                            allCharcters[characterPlaceInList].mySituations.Add(newLine);
                        }
                        else
                        {
                            allCharcters[characterPlaceInList].mySituations[releventStatePlace].isHappand = newStatus.isHappand;
                        }
                    }
                }
            }
            return allCharcters;
        }

        [Header("Play Event")]
        public bool willCallEvent = false;
        public GameEvent Event;

        [Header("WillHelpFinishLevel")]
        public bool WillHelpFinishLevel = false;
    }
}

[System.Serializable]
public class ChangeAnimation
{
    public ObjectName Character;
    public int CharacterID;
    public string AnimationName = "Animation";
    public int AnimationNum;
}

[System.Serializable]
public class CharacterStatus
{
    public enum RelationshipType { Friendship,NeedHelp, Scared, situation1, situation2, situation3 };
    public RelationshipType relationshipType = new RelationshipType();
    public ObjectName mainObject;
    public ObjectName secondaryObject;
    public int mainCharacteID;
    public int secondaryCharacterID;
    public bool isHappand = true;
}
[System.Serializable]
public class MainCharcter
{
    public int ID;
    public List<CharacterStatus> mySituations = new List<CharacterStatus>();
}

public enum Character { Alligator, Bear, Giraffe, Hedgehog, Panda, Rhinoceros }

public enum ObjectType { Background, Character }

//public class CharacterStangeInfo
//{
//    public int ChractersIDs;
//    public int ChractersObjects;
//    public int ChractersColliderID;
//    public void update (int ChractersIDs, int ChractersObjects, int ChractersColliderID)
//    {
//        CharacterStangeInfo newLine = new CharacterStangeInfo();
//        newLine.ChractersIDs = ChractersIDs;
//    }
//}