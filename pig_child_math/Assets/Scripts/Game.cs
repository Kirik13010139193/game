using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

public class Game : MonoBehaviour
{
    public List<quest> Questions = new List<quest>()
    {
       new quest()
       {
           question = "1+2",
           answers = new string[]{"4","1","3","0"},
           correctIndex = 2
       },
       new quest()
       {
           question = "2+3",
           answers = new string[]{"1","5","3","4"},
           correctIndex = 1
       },
       new quest()
       {
           question = "2+2",
           answers = new string[]{"4","5","2","0"},
           correctIndex = 0
       },
       new quest()
       {
           question = "1+2",
           answers = new string[]{"4","1","3","0"},
           correctIndex = 2
       },
       new quest()
       {
           question = "5+1",
           answers = new string[]{"4","1","6","3"},
           correctIndex = 2
       },
       new quest()
       {
           question = "1+4",
           answers = new string[]{"3","2","7","5"},
           correctIndex = 3
       },
       new quest()
       {
           question = "6+1",
           answers = new string[]{"5","3","7","9"},
           correctIndex = 2
       },
       new quest()
       {
           question = "8+2",
           answers = new string[]{"6","5","1","10"},
           correctIndex = 3
       },
       new quest()
       {
           question = "7+3",
           answers = new string[]{"6","4","10","9"},
           correctIndex = 2
       },
       new quest()
       {
           question = "9+2",
           answers = new string[]{"7","11","10","9"},
           correctIndex = 1
       },
    };
    
    public Sprite secondBack;
    public AudioClip[] animalsSounds;
    //Кошка
    //Овца
    //Собака
    //Свинья
    //Курица
    public Sprite[] animals;
    //Слон
    //Кошка
    //Овца
    //Собака
    //Корова
    //Свинья
    //Курица
    private List<AnimalsSounds> listANim = new List<AnimalsSounds>();
    private int indexOfQuest = 0;
    private int indexAnimal = 0;
    public TMP_Text question_text;
    public GameObject prefabButton,content,back,playButton,won,exitGame_,task;
    public AudioSource auds;
    public AudioClip but,yes;
    public Animator anim_peppa;
    private void Awake()
    {
        listANim.Add(new AnimalsSounds()
        {
            an = animals[0],
            anClip = null,
            name = "слон"
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[1],//Кошка
            anClip = animalsSounds[0],
            name = "кошка"
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[2],//Овца
            anClip = animalsSounds[1],
            name = "овца"
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[3],//Собака
            anClip = animalsSounds[2],
            name = "собака"
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[4],//Корова
            anClip = null,
            name = "корова",
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[5],//Свинья
            anClip = animalsSounds[3],
            name = "свинья"
        });
        listANim.Add(new AnimalsSounds()
        {
            an = animals[6],//Курица
            anClip = animalsSounds[4],
            name = "курица"
        });
        renderGame();
    }

    void renderGame()
    {
        question_text.text = Questions[indexOfQuest].question;
        renderButtonsMath(Questions[indexOfQuest].answers,Questions[indexOfQuest].correctIndex);
    }

    private void renderButtonsMath(string[] answers,int index)
    {

        for (var i = 0; i < answers.Length; i++)
        {
            int d = i;
            GameObject but = Instantiate(prefabButton, content.transform);
            Button but_cmp = but.GetComponent<Button>();
            but.transform.GetChild(0).GetComponent<TMP_Text>().text = answers[i];
            but_cmp.onClick.AddListener(() => OnClick(d,index));
        }
    }

    public void OnClick(int id,int correct_id)
    {
        if (id != correct_id)
        {
            auds.clip = but;
            auds.Play();
        }
        else
        {
            for (var i = 0; i < content.transform.childCount; i++)
            {
                GameObject d = content.transform.GetChild(i).gameObject;
                Destroy(d);
            }
            auds.clip = yes;
            auds.Play();
            anim_peppa.SetBool("anim",true);
            StartCoroutine(nextIndex());
        }
    }


    IEnumerator nextIndex()
    {
        yield return new WaitForSeconds(1);
        anim_peppa.SetBool("anim",false);
        indexOfQuest++;
        if (indexOfQuest <= Questions.Count - 1)
        {
            renderGame();
        }
        else
        {
            back.GetComponent<Image>().sprite = secondBack;
            playButton.SetActive(true);
            question_text.text = "Что за животное?";
            playButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                auds.clip = listANim[1].anClip;
                auds.Play();
            });
            var list = shuffleList(listANim);
            bool yes_ = false;
            for (var i = 0; i < 4; i++)
            {
                int d = i;
                GameObject but = Instantiate(prefabButton, content.transform);
                but.GetComponent<Image>().sprite = list[i].an;
                but.GetComponent<Image>().color = Color.white;
                Destroy(but.transform.GetChild(0).gameObject);
                Button but_cmp = but.GetComponent<Button>();
                if (list[i].name == "кошка")
                {
                    but_cmp.onClick.AddListener(() =>
                    {
                        for (var i = 0; i < content.transform.childCount; i++)
                        {
                            GameObject d = content.transform.GetChild(i).gameObject;
                            Destroy(d);
                        }
                        auds.clip = yes;
                        auds.Play();
                        anim_peppa.SetBool("anim",true);
                        StartCoroutine(off());
                    });
                    yes_ = true;
                }
               
            }
            if (!yes_)
            {
                var but = content.transform.GetChild(1).gameObject;
                but.GetComponent<Image>().sprite = listANim[1].an;
                Button but_cmp = but.GetComponent<Button>();
                but_cmp.onClick.AddListener(() =>
                {
                    for (var i = 0; i < content.transform.childCount; i++)
                    {
                        GameObject d = content.transform.GetChild(i).gameObject;
                        Destroy(d);
                    }
                    auds.clip = yes;
                    auds.Play();
                    anim_peppa.SetBool("anim",true);
                    StartCoroutine(off());
                });
            }
        }
        yield return null;
    }

    IEnumerator off()
    {
        yield return new WaitForSeconds(.5f);
        anim_peppa.SetBool("anim",false);
        if (indexAnimal == 0)
        {
            playButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                auds.clip = listANim[2].anClip;
                auds.Play();
            });
            var list = shuffleList(listANim);
            bool yes_ = false;
            for (var i = 0; i < 4; i++)
            {
                int d = i;
                GameObject but = Instantiate(prefabButton, content.transform);
                but.GetComponent<Image>().sprite = list[i].an;
                but.GetComponent<Image>().color = Color.white;
                Destroy(but.transform.GetChild(0).gameObject);
                Button but_cmp = but.GetComponent<Button>();
                if (list[i].name == "овца")
                {
                    but_cmp.onClick.AddListener(() =>
                    {
                        for (var i = 0; i < content.transform.childCount; i++)
                        {
                            GameObject d = content.transform.GetChild(i).gameObject;
                            Destroy(d);
                        }
                        auds.clip = yes;
                        indexAnimal++;
                        auds.Play();
                        anim_peppa.SetBool("anim",true);
                        StartCoroutine(off());
                    });
                    yes_ = true;
                }
               
            }
            if (!yes_)
            {
                var but = content.transform.GetChild(0).gameObject;
                but.GetComponent<Image>().sprite = listANim[2].an;
                Button but_cmp = but.GetComponent<Button>();
                but_cmp.onClick.AddListener(() =>
                {
                    for (var i = 0; i < content.transform.childCount; i++)
                    {
                        GameObject d = content.transform.GetChild(i).gameObject;
                        Destroy(d);
                    }

                    indexAnimal++;
                    auds.clip = yes;
                    auds.Play();
                    anim_peppa.SetBool("anim",true);
                    StartCoroutine(off());
                });
            }
        }
        else if(indexAnimal == 1)
        {
            playButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                auds.clip = listANim[3].anClip;
                auds.Play();
            });
            var list = shuffleList(listANim);
            bool yes_ = false;
            for (var i = 0; i < 4; i++)
            {
                int d = i;
                GameObject but = Instantiate(prefabButton, content.transform);
                but.GetComponent<Image>().sprite = list[i].an;
                but.GetComponent<Image>().color = Color.white;
                Destroy(but.transform.GetChild(0).gameObject);
                Button but_cmp = but.GetComponent<Button>();
                if (list[i].name == "собака")
                {
                    but_cmp.onClick.AddListener(() =>
                    {
                        for (var i = 0; i < content.transform.childCount; i++)
                        {
                            GameObject d = content.transform.GetChild(i).gameObject;
                            Destroy(d);
                        }

                        indexAnimal++;
                        auds.clip = yes;
                        auds.Play();
                        anim_peppa.SetBool("anim",true);
                        StartCoroutine(off());
                    });
                    yes_ = true;
                }
               
            }
            if (!yes_)
            {
                var but = content.transform.GetChild(3).gameObject;
                but.GetComponent<Image>().sprite = listANim[3].an;
                Button but_cmp = but.GetComponent<Button>();
                but_cmp.onClick.AddListener(() =>
                {
                    for (var i = 0; i < content.transform.childCount; i++)
                    {
                        GameObject d = content.transform.GetChild(i).gameObject;
                        Destroy(d);
                    }

                    indexAnimal++;
                    auds.clip = yes;
                    auds.Play();
                    anim_peppa.SetBool("anim",true);
                    StartCoroutine(off());
                });
            }
        }
        else
        {
            task.SetActive(false);
            won.SetActive(true);
            exitGame_.SetActive(true);
        }
    }

    public void exitGame()
    {
        SceneManager.LoadScene(0);
    }
    private static Random random = new Random();  
    public List<AnimalsSounds> shuffleList(List<AnimalsSounds> a)
    {
        //var data = a;
       var data = new List<AnimalsSounds>();
        foreach (var s in a)
        {
            int j = random.Next(data.Count + 1);
            if (j == data.Count)
            {
                data.Add(s);
            }
            else
            {
                data.Add(data[j]);
                data[j] = s;
            }
        }
        return data;
    }
}
[Serializable]
public class AnimalsSounds
{
    public Sprite an;
    public AudioClip anClip;
    public string name = "";
}
[Serializable]
public class quest
{
    public string question;
    public string[] answers;
    public int correctIndex;
}