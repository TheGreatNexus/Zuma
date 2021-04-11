using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { gameMenu, gamePlay, gamePause }

public class GameManager : Manager<GameManager>
{

    private List<GameObject> ballList = new List<GameObject>();
    //Game State
    private GameState m_GameState;
    public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }

    // TIME SCALE
    private float m_TimeScale;
    public float TimeScale { get { return m_TimeScale; } }
    void SetTimeScale(float newTimeScale)
    {
        m_TimeScale = newTimeScale;
        Time.timeScale = m_TimeScale;
    }

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();

        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
        EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.AddListener<BallHasBeenAddedToQueueEvent>(BallHasBeenAddedToQueue);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();

        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
        EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.RemoveListener<BallHasBeenAddedToQueueEvent>(BallHasBeenAddedToQueue);
    }
    #endregion

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
    {
        Menu();
        yield break;
    }
    #endregion

    #region Callbacks to Events issued by MenuManager

    private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        SceneManager.LoadScene("zuma");
    }

    private void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }
    private void QuitButtonClicked(QuitButtonClickedEvent e)
    {
        Quit();
    }

    private void ResumeButtonClicked(ResumeButtonClickedEvent e)
    {
        Resume();
    }

    private void EscapeButtonClicked(EscapeButtonClickedEvent e)
    {
        if (IsPlaying)
            Pause();
    }
    #endregion

    //EVENTS

    private void Menu()
    {
        SetTimeScale(0);
        m_GameState = GameState.gameMenu;
        EventManager.Instance.Raise(new GameMenuEvent());
    }

    private void Play()
    {
        // InitNewGame();
        SetTimeScale(1);
        m_GameState = GameState.gamePlay;
        EventManager.Instance.Raise(new GamePlayEvent());
    }

        private void Quit()
    {
        EventManager.Instance.Raise(new GameQuitEvent());
    }

    private void Pause()
    {
        SetTimeScale(0);
        m_GameState = GameState.gamePause;
        EventManager.Instance.Raise(new GamePauseEvent());
    }

    private void Resume()
    {
        SetTimeScale(1);
        m_GameState = GameState.gamePlay;
        EventManager.Instance.Raise(new GameResumeEvent());
    }

    //Utilities functions
    public bool getGameState()
    {
        if (IsPlaying)
        {
            return true;
        }
        else return false;
    }

    private void InitNewGame()
    {
        // a faire
    }
    private void BallHasBeenAddedToQueue(BallHasBeenAddedToQueueEvent e)
    {
        ballList.Add(e.ball);
    }
}
