using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static CharacterStatus;

public class LevelManagerCode : MonoBehaviour
{
    [SerializeField] List<DropSlot> SliderOrder = new List<DropSlot>();
    [SerializeField] List<ItemCanDrag> allCharacters = new List<ItemCanDrag>();

    [SerializeField] List<MainCharcter> CharactersStatusTraker = new List<MainCharcter>();

    [SerializeField] List<Condition> Defult = new List<Condition>();
    [SerializeField] List<Condition> conditions = new List<Condition>();
    [SerializeField] Animator IconFinalLevel;
    private void Start()
    {

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
                for (int i =0; i< slide.backgroundManager.objectInSlotList.Count;i++)
                {
                    print("EndStartLoop");
                    GameObject thisObject = slide.backgroundManager.objectInSlotList[i];
                    if (thisObject != null)
                    {
                        ChractersIDs.Add(thisObject.GetComponent<ItemCanDrag>().objectID);
                        ChractersObjects.Add(thisObject);
                    }
                }
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
                        if (condition.WillHelpFinishLevel)
                        {
                            print("FinishLevelnow: " + ProgressFinishLevel.ToString() + " Goal: " + FinishLevelGoal.ToString());
                            ProgressFinishLevel++;
                            if (ProgressFinishLevel == FinishLevelGoal)
                            {
                                FinishLevel();
                            }
                        }
                    }
                }
            }
        }
    }
    
    public void FinishLevel()
    {
        print("FinishLevel");
        if (IconFinalLevel!=null)
        {
            IconFinalLevel.SetBool("End", true);
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
                if (characterStatuses[i].secondaryCharacterID == newState.secondaryCharacterID && characterStatuses[i].relationshipType == newState.relationshipType)
                {
                    return i;
                }
            }
            return -1;
        }

       

        [Header("---Couse---")]
        [Header("Chack Situation")]
        public bool isSituation;
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
                    print("i"+i);
                    print("get to secend if:" + (CharactersIDs.Contains(id) == false).ToString());

                    bool Contain = false;
                    if (useSpacificSlot)
                    {
                        Contain = CharacterID[i] == id;
                    }
                    else
                    {
                        Contain = CharacterID.Contains(id) == false;
                    }
                    if (Contain)
                    {
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
        [System.Serializable]
        public class ChangeAnimation
        {
            public int CharacterID;
            public string AnimationName = "Animation";
            public int AnimationNum;
        }
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
                    else if (newStatus.relationshipType == RelationshipType.NeedHelp || newStatus.relationshipType == RelationshipType.Scared)
                    {
                        int releventStatePlace = GetRelevantStatePlace(allCharcters[characterPlaceInList].mySituations, newStatus);
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

        [Header("WillHelpFinishLevel")]
        public bool WillHelpFinishLevel = false;
    }
}
[System.Serializable]
public class CharacterStatus
{
    public enum RelationshipType { Friendship,NeedHelp, Scared };
    public RelationshipType relationshipType = new RelationshipType();
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