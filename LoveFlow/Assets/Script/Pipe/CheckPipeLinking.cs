using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPipeLinking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PipeController>() == null) return;        
        // T Pipe
        if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_T)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지           
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "RightwardCollider ") || string.Equals(gameObject.name, "RightwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Rightward] = other.GetComponentInParent<PipeController>().gameObject;
            }
        }

        // Straight Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Straight)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.transform.name, "BackwardCollider ") || string.Equals(gameObject.transform.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = other.GetComponentInParent<PipeController>().gameObject;
            }
        }

        // Elbow Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Elbow)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = other.GetComponentInParent<PipeController>().gameObject;
            }
        }

        // Cross Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Cross)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "RightwardCollider ") || string.Equals(gameObject.name, "RightwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Rightward] = other.GetComponentInParent<PipeController>().gameObject;
            }
            else if (string.Equals(gameObject.name, "LeftwardCollider ") || string.Equals(gameObject.name, "LeftwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Leftward] = other.GetComponentInParent<PipeController>().gameObject;        
            }
        }
        // Start Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start1 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start2 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start3 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start4)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지           
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
                Debug.Log(other.GetComponentInParent<PipeController>().gameObject);
            }
        }
        // End Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End1 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End2 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End3 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End4)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지           
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = other.GetComponentInParent<PipeController>().gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PipeController>() == null) return;
        // T Pipe
        if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_T)
        {
            gameObject.GetComponentInParent<PipeController>()._linkedCount--;
            // 어느 구멍이 해제되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = null;       
            }
            else if (string.Equals(gameObject.name, "RightwardCollider ") || string.Equals(gameObject.name, "RightwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Rightward] = null;
            }
        }

        // Straight Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Straight)
        {
            gameObject.GetComponentInParent<PipeController>()._linkedCount--;
            // 어느 구멍이 해제되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = null;
            }
        }

        // Elbow Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Elbow)
        {
            gameObject.GetComponentInParent<PipeController>()._linkedCount--;
            // 어느 구멍이 해제되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = null;
            }
        }

        // Cross Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Cross)
        {
            gameObject.GetComponentInParent<PipeController>()._linkedCount--;
            // 어느 구멍이 해제되었는지
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
            else if (string.Equals(gameObject.name, "BackwardCollider ") || string.Equals(gameObject.name, "BackwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Backward] = null;
            }
            else if (string.Equals(gameObject.name,"RightwardCollider ") || string.Equals(gameObject.name, "RightwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Rightward] = null;
            }
            else if (string.Equals(gameObject.name, "LeftwardCollider ") || string.Equals(gameObject.name, "LeftwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Leftward] = null;
            }
        }

        // Start Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start1 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start2 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start3 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_Start4)
        {           
            gameObject.GetComponentInParent<PipeController>()._linkedCount--;
            // 어느 구멍이 연결되었는지           
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
        }
        // End Pipe
        else if (gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End1 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End2 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End3 ||
            gameObject.GetComponentInParent<PipeController>()._pipeType == PipeController.EPipeType.Pipe_End4)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = true;
            gameObject.GetComponentInParent<PipeController>()._linkedCount++;
            // 어느 구멍이 연결되었는지           
            if (string.Equals(gameObject.name, "ForwardCollider ") || string.Equals(gameObject.name, "ForwardCollider"))
            {
                gameObject.GetComponentInParent<PipeController>()._linkedPipeList[(int)PipeController.LinkedDirection.Forward] = null;
            }
        }

        // 완전히 연결 해제
        if (gameObject.GetComponentInParent<PipeController>()._linkedCount <= 0)
        {
            gameObject.GetComponentInParent<PipeController>()._isLinked = false;
        }
    }
   
}
