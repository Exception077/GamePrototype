/*********************************************************************************
 *Copyright(C) 2015 by Gx
 *All rights reserved.
 *FileName:     GameCharacterManager.cs
 *Author:       Gx
 *Version:      1.0
 *UnityVersion：2017.3.1f1
 *Date:         2019-01-15
 *Update:
 *Description:   
 *History:  
**********************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class GameCharacterManager : MonoBehaviour{
    public List<NPC> NPCList = new List<NPC>();
    public List<Enemy> EnemyList = new List<Enemy>();
    public List<Player> PlayerList = new List<Player>();
    public List<GameCharacter> BodyList = new List<GameCharacter>();
    public static GameCharacterManager MyInstance;
    private static object syncRoot = new Object();
    public static GameCharacterManager Instance {
        get {
            if (MyInstance == null) {
                lock (syncRoot) {
                    if (MyInstance == null) {
                        GameCharacterManager[] MyInstances = FindObjectsOfType<GameCharacterManager>();
                        if (MyInstances != null) {
                            for (var i = 0; i < MyInstances.Length; i++) {
                                Destroy(MyInstances[i].gameObject);
                            }
                        }
                        GameObject go = new GameObject("GameCharacterManager");
                        MyInstance = go.AddComponent<GameCharacterManager>();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return MyInstance;
        }
    }

    public GameCharacter getCharacter(string name) {
        foreach(NPC n in NPCList) {
            if(n.CharacterName == name) {
                return n;
            }
        }
        foreach(Enemy e in EnemyList) {
            if(e.CharacterName == name) {
                return e;
            }
        }
        foreach(Player p in PlayerList) {
            if(p.CharacterName == name) {
                return p;
            }
        }
        return null;
    }
    public void killCharacter(GameCharacter g) {
        BodyList.Add(g);
    }

    public void reviveCharacter(GameCharacter g) {
        foreach(GameCharacter gc in BodyList) {
            if(gc.CharacterName == g.CharacterName) {
                BodyList.Remove(gc);
                break;
            }
        }
    }

    public bool contaiinsBody(GameCharacter g) {
        foreach(GameCharacter gc in BodyList) {
            if(gc.CharacterName == g.CharacterName) {
                return true;
            }
        }
        return false;
    }

    public void addNPC(NPC npc) {
        NPCList.Add(npc);
    }
    public void removeNPC(NPC npc) {
        for (int i = 0; i < NPCList.Count; i++) {
            if (NPCList[i] == null) {
                NPCList.RemoveAt(i);
                
            } else if (NPCList[i].CharacterName == npc.CharacterName) {
                NPCList.RemoveAt(i);
                break;
            }
        }
    }
    public bool containNPC(NPC npc) {
        for (int i = 0; i < NPCList.Count; i++) {
            if (NPCList[i] == null) {
                NPCList.RemoveAt(i);
            } else if (NPCList[i].CharacterName == npc.CharacterName) {
                return true;
            }
        }
        return false;
    }
    public void addEnemy(Enemy enemy) {
        EnemyList.Add(enemy);
    }
    public void removeEnemy(Enemy enemy) {
        for (int i = 0; i < EnemyList.Count; i++) {
            if (EnemyList[i] == null) {
                EnemyList.RemoveAt(i);
            } else if (EnemyList[i].CharacterName == enemy.CharacterName) {
                EnemyList.RemoveAt(i);
            }
        }
    }
    public bool containEnemy(Enemy enemy) {
        for (int i = 0; i < EnemyList.Count; i++) {
            if (EnemyList[i] == null) {
                EnemyList.RemoveAt(i);
            } else if (EnemyList[i].CharacterName == enemy.CharacterName) {
                return true;
            }
        }
        return false;
    }
    public void addPlayer(Player player) {
        PlayerList.Add(player);
    }
    public void removePlayer(Player player) {
        for (int i = 0; i < PlayerList.Count; i++) {
            if(PlayerList[i] == null) {
                PlayerList.RemoveAt(i);
            } else if (PlayerList[i].CharacterName == player.CharacterName) {
                PlayerList.RemoveAt(i);
                break;
            }
        }
    }
    public bool containPlayer(Player player) {
        for(int i = 0; i < PlayerList.Count; i++) {
            if (PlayerList[i] == null) {
                PlayerList.RemoveAt(i);
            } else if (PlayerList[i].CharacterName == player.CharacterName) {
                return true;
            }
        }
        return false;
    }
}