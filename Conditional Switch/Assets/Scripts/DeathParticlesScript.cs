/*
Enables particles on death
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticlesScript : MonoBehaviour
{
    public Transform player;

    public void runParticles()
    {
        transform.position = player.position;
        gameObject.SetActive(true);
    }
}
