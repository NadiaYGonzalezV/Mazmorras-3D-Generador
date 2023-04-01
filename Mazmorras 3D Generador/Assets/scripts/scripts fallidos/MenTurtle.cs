using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidFuncVoid(); //delegados
public delegate void VoidFuncInt(int turn);
public delegate void VoidFunc4Param(Vector3 pos, Vector3 lastPos, int turn, int invTurn);

public class MenTurtle 
{
   
        Vector3 pos = Vector3.zero;
        Vector3 lastPos = Vector3.zero;

        int turn = 0;
        int invTurn = 0;

        Vector3[] dirs =
        {
            Vector3.up,
            Vector3.right,
            Vector3.down,
            Vector3.left
        };

        float clampX = 0;
        float clampY = 0;

        public Vector3 Pos { get => pos; }
        public Vector3 LastPos { get => lastPos; }
        public Vector3 Dir { get => dirs[turn]; }

        public int Turn { get => turn; }
        public int InvTurn { get => invTurn; }

        public VoidFunc4Param forwardDelegate;
        public VoidFunc4Param backwardDelegate;
        public VoidFuncInt turnDelegate;

        public MenTurtle(float clampX = 3, float clampY = 3)
        {
            forwardDelegate = (x, y, z, w) => { };
            backwardDelegate = (x, y, z, w) => { };
            turnDelegate = (i) => { };

            invTurn = (turn + 2) & 0x03;

            this.clampX = clampX;
            this.clampY = clampY;
        }

        public void Forward()
        {
            lastPos = pos;

            pos += dirs[turn];

            pos.x = Mathf.Clamp(pos.x, 0, clampX - 1);
            pos.y = Mathf.Clamp(pos.y, 0, clampY - 1);

            if (lastPos != pos)
            {
                forwardDelegate(pos, lastPos, turn, invTurn);
            }
        }

        public void Backwadr()
        {
            lastPos = pos;

            pos += dirs[invTurn];

            pos.x = Mathf.Clamp(pos.x, 0, clampX - 1);
            pos.y = Mathf.Clamp(pos.y, 0, clampY - 1);

            if (lastPos != pos)
            {
                backwardDelegate(pos, lastPos, turn, invTurn);
            }
        }

        public void Forward(int fwd)
        {
            for (int i = 1; i <= fwd; i ++)
            {
                Forward();
            }
        }

        public void TurnTo(int newTurn)
        {
            turn = newTurn;
            turn &= 0x03;
            invTurn = (turn + 2) & 0x03;
            turnDelegate(turn);
        }

        public void AddTurn(int newTurn)
        {
            turn += newTurn;
            turn &= 0x03;
            invTurn = (turn + 2) & 0x03;
            turnDelegate(turn);
        }

        public void SetPos(float x, float y)
        {
            pos.x = x;
            pos.y = y;

            lastPos = pos;

            TurnTo(0);
        }
        public void SetClamp(float clampX, float clampY)
        {
            this.clampX = clampX;
            this.clampX = clampY;
        }
}//clase

