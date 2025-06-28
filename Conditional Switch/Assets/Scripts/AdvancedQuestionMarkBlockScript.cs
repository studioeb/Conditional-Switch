/*
Control script for advanced question mark blocks
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

using UnityEngine;

public class AdvancedQuestionMarkBlockScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    public LogicSystemScript logic;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicSystemScript>();
    }

    private void Update()
    {
        if (logic.isDead == false & logic.isPaused == false & logic.gameHasStarted)
        {
            transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
            transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, (1 * rotationSpeed) * Time.deltaTime));

            if (transform.position.x < -20)
            {
                Destroy(gameObject);
            }
        }
    }
}
