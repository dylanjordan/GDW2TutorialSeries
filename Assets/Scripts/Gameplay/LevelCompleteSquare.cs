using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteSquare : MonoBehaviour
{
    [SerializeField] List<Sprite> powerupList;

    float _changeTimer = 0;

    int _previousChoice = 5;

    bool _itemCollected;

    void Update()
    {
        if (!_itemCollected)
        {
            ChangeImage();
        }
    }

    void ChangeImage()
    {
        if (_changeTimer < Time.realtimeSinceStartup)
        {
            int choice = (int)Random.Range(0, powerupList.Count - 1);

            if (choice == _previousChoice)
            {
                ChangeImage();
                return;
            }

            GetComponent<SpriteRenderer>().sprite = powerupList[choice];

            _changeTimer = Time.realtimeSinceStartup + 0.25f;

            _previousChoice = choice;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _itemCollected = true;
        FindObjectOfType<LevelTimer>().TimerAddScore();
        FindObjectOfType<LevelStatus>().SetLevelComplete(true);
        StartCoroutine(TimeBeforeScene());
        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("LevelClear");
    }

    public IEnumerator TimeBeforeScene()
    {
        yield return new WaitForSeconds(3);
    }
}
