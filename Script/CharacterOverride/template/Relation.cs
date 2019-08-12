using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Impression {
    public string Name;
    public int Process;
}
public class Relation : MonoBehaviour {
    [SerializeField]
    readonly List<Impression> Impressions = new List<Impression>();

    /// <summary>
    /// ����µĽ�ɫ�ø���Ϣ
    /// </summary>
    /// <param name="name">��ɫ��</param>
    /// <param name="process">�øж�</param>
    public void addImpression(string name, int process) {
        Impression impression = new Impression();
        impression.Name = name;
        impression.Process = process;
        Impressions.Add(impression);
    }

    /// <summary>
    /// ��ѯ�øж�
    /// </summary>
    /// <param name="name">��ɫ��</param>
    /// <returns></returns>
    public int getImpression(string name) {
        int process = 0;
        foreach (Impression i in Impressions) {
            if (i.Name == name) {
                process = i.Process;
            }
        }
        return process;
    }

    /// <summary>
    /// �޸ĺøж�
    /// </summary>
    /// <param name="name">��ɫ��</param>
    /// <param name="process">�ø�</param>
    public void setImpression(string name, int process) {
        Impressions.ForEach(i =>
        {
            if (i.Name == name) {
                i.Process = process;
            }
        });
    }
}
