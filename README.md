The gameplay consists of defending your tower by enemy’s attacks. The game environment 
is composed by a terrain with your headquarter tower placed in a certain position. You can 
place other defence towers around your headquarter in order to block the enemy’s attack. 
Enemy’s troops will have a basic behaviour: get close to a target, start shooting and go 
to another target once it has been destroyed. The game ends once all enemies have 
been killed (win) or the headquarter is destroyed (lose).

The camera should look at the scene as the following ref image from “Boom Beach”

The main gameplay flow is made of two phases:
- Planning phase: the user can place a limited number of defence towers by 
dragging them on the field and/or move them around. Moreover the user can pan 
(double touch) and zoom (pinch) the camera to have a better view.
- Fighting phase: the user hits the Fight button and the simulation starts: enemies 
begin to attack user’s towers, that will react according to their parameters.

Each tower belongs to one of these categories:
- Guard: MED Resistance, LOW Firepower, HIGH Shot frequency, MED Shot range 
- Sniper: LOW Resistance, HIGH Firepower, MED Shot frequency, HIGH Shot range 
- Mortar: HIGH Resistance, HIGH Firepower, LOW Shot frequency, MED Shot range 
(will not shot under a certain distance )

The delivery consists of an archive file (name-surname.zip) containing the Unity project, and 
the APK file of the Android build. Please include only Assets and Project Settings folders in 
the archive.

The project must use Unity 2021.3.4f1 (https://unity3d.com/get-unity/download/archive).
Mandatory features:
- Basic 3D graphics 
- Three different defence towers.
- Both gameplay: plan and fighting.
- Simple enemies.
- Basic UI (start page, ingame (edit mode + fighting mode), reward page).
- Mobile version with touch input.
- Mines: they explode when the user tap on them damaging the enemies in a certain 
range.

Optional features (in order of importance):
- Advanced enemies (different categories with different speed, resistance, firepower, 
etc…)
- Fences: they are physical obstacles that the enemies must avoid.
- During the Fighting Phase the simulation can be speeded up (2x) using a toggle 
button
- Advanced graphics.
- Special effects.
- Sound effects.

Your work will be evaluated by considering following aspects:
- Quality and completeness of the resulting project.
- Quality of the code delivered.
- Number and quality of the optional features implemented.
