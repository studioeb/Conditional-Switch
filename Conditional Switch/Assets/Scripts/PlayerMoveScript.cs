/*
Controls player movement
Copyright (C) 2025 Ethan Bayer

This file is part of Conditional Switch.

Conditional Switch is free software: you can redistribute it and/or
modify it under the terms of the GNU General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Conditional Switch is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveScript : MonoBehaviour
{
    public PlayerControls controls;
    public Rigidbody2D rb;

    public LogicSystemScript logic;

    public float gravityScale = 3f;

    // Max regular: 9
    //Max adv: 2
    public int regularQuestionNumber = 0;
    public int advQuestionNumber = 0;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Movement.SwitchGravity.performed += ctx => Switch();
        controls.Movement.Pause.performed += ctx => logic.pauseGame();
    }

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicSystemScript>();
        transform.position = new Vector3(-3, 2, 0);
    }

    private void Switch()
    {
        if (!logic.isPaused & logic.gameHasStarted & !IsPointerOverPauseButton())
        {
            rb.gravityScale = 0 - rb.gravityScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Saw":
                if (!logic.isPresentationMode)
                {
                    logic.isDead = true;
                    logic.gameOver();
                    gameObject.SetActive(false);
                }
                break;
            case "Boundary":
                if (!logic.isPresentationMode)
                {
                    logic.isDead = true;
                    logic.gameOver();
                    gameObject.SetActive(false);
                }
                break;
            case "QuestionMarkBlock":
                regularQuestionNumber++;
                logic.askQuestion(false);
                break;
            case "AdvancedQuestionMarkBlock":
                advQuestionNumber++;
                logic.askQuestion(true);
                break;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public static bool IsPointerOverPauseButton()
    {
        PointerEventData currentEventData = new PointerEventData(EventSystem.current);
        currentEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(currentEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++) // Loops through raycastResults, to get value use raycastResults[i]
        {
            if (raycastResults[i].gameObject.layer == 6) // Layer 6 is the pause button layer
            {
                return true;
            }
        }

        return false;
    }
}