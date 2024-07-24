using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region PUBLICS

    public FieldGenerator _FieldGenerator;

    [Space(10)]

    public Text _TXTPlayerLives;
    public Text _TXTGameState;

    [Space(10)]

    public GameObject[] _Skulls = new GameObject[2];

    [Space(10)]

    public AudioSource _AudioSource;

    [Space(10)]

    public AudioClip _SoundDefeat;
    public AudioClip _SoundVictory;

    #endregion

    #region PRIVATES

    private int _playerLives = 3;

    private bool _isGameOn = false;
    private bool _GameWon = false;
    private bool _playerAAlive = false;
    private Vector3 _tempVec = Vector3.zero;
    private float _skullRiseTime = 4f;
    private float _initialSkullHeight = -1f;

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        _TXTGameState.color = Color.red;
        _TXTGameState.text = "";
        ResetSkulls();
    }

    #endregion

    #region METHODS

    public void ResetGame()
    {
        _playerLives = 3;

        _TXTGameState.text = "IDLE";
        _TXTGameState.color = Color.white;
        _FieldGenerator.RemoveAllPlateaus();

        _isGameOn = false;
        _GameWon = false;

        StartGame();
    }

    public void StopGame()
    {
        if (!_isGameOn) return;

        _AudioSource.PlayOneShot(_SoundDefeat);

        _isGameOn = false;

        _TXTGameState.text = "GAME OVER";
        _TXTGameState.color = Color.red;

        _FieldGenerator.SubmergeAllPlateaus();
        LetDemSkullsRise();
    }

    public void GameWon()
    {
        //_AudioSource.clip = _SoundVictory;
        _AudioSource.PlayOneShot(_SoundVictory);

        _isGameOn = false;
        _GameWon = true;

        _TXTGameState.text = "YOU'RE WINNER";
        _TXTGameState.color = Color.green;

        _playerAAlive = true;
        _playerLives = 999;

        _FieldGenerator.SurfacePath();
    }

    public void StartGame()
    {
        if (_isGameOn) return;

        _GameWon = false;

        _playerLives = 3;
        _playerAAlive = true;

        _TXTGameState.text = "GAME ON";
        _TXTGameState.color = Color.green;

        _FieldGenerator.GenerateField();
        _FieldGenerator.MirrorPlateauA(true);

        ResetSkulls();

        _isGameOn = true;  
    }

    public void PlayerSteppedOnHole(int type)
    {
        Debug.Log("Player " + type + " stepped on hole", this);

        //_AudioSource.PlayOneShot(_SoundPlateauHole);

        if (type == 1)
        {
            StopGame();
            return;
        }
        else if (type == 0 && _playerAAlive)
        {
            _playerLives--;
            _TXTPlayerLives.text = "";

            for (int i = 0; i < _playerLives; i++) _TXTPlayerLives.text += "X";

            if (_playerLives == 0 && _playerAAlive)
            {
                _playerAAlive = false;
                if (!_GameWon) _FieldGenerator.SubmergePlateausA();
                _FieldGenerator.MirrorPlateauA(false);
            }
        }
    }

    private void LetDemSkullsRise()
    {
        StartCoroutine(RiseSkull());
    }

    private IEnumerator RiseSkull()
    {
        ResetSkulls();

        while (_Skulls[0].transform.position.y <= 0f)
        {
            for (int i = 0; i < _Skulls.Length;  i++)
            {
                _tempVec = _Skulls[i].transform.position;
                _tempVec.y += Time.deltaTime / _skullRiseTime;
                _Skulls[i].transform.position = _tempVec;
            }

            yield return null;
        }
    }

    private void ResetSkulls()
    {
        for (int i = 0; i < _Skulls.Length; i++)
        {
            _Skulls[i].transform.localPosition = new Vector3(0f, _initialSkullHeight, 0f);
        }
    }

    #endregion
}
