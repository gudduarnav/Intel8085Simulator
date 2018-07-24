     LXI H, E100H
     LXI D, E105H
     LXI B, 000AH

     DAD B
     DCX H

     XCHG
     DAD B
     DCX H
     XCHG

LOOP: MOV A, M
     XCHG
     MOV M, A
     XCHG 

     DCX H
     DCX D

     DCR C
     JNZ LOOP

     HLT