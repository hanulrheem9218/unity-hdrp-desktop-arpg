using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(enemyShooterAI))]

public class enemyShooterAIEditor :  Editor
{
    private void OnSceneGUI()
    {

        enemyShooterAI shooter = (enemyShooterAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(shooter.shooterObject.transform.position, Vector3.up, Vector3.forward, 360, shooter.viewRadius);
        Vector3 viewAngleA = shooter.DirFromAngle(-shooter.viewAngle / 2, false);
        Vector3 viewAngleB = shooter.DirFromAngle(shooter.viewAngle / 2, false);

        Handles.DrawLine(shooter.shooterObject.transform.position, shooter.shooterObject.transform.position + viewAngleA * shooter.viewRadius);
        Handles.DrawLine(shooter.shooterObject.transform.position, shooter.shooterObject.transform.position + viewAngleB * shooter.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in shooter.visibleTargets)
        {
            Handles.DrawLine(shooter.shooterObject.transform.position, visibleTarget.position);
        }
    }
}
#endif
