     LXI H, E100H
     LXI D, E200H
     MOV C, M

LOOP: INX H
     INX D
     MOV A, M
     XCHG
     MOV M, A
     XCHG

     DCR C
     JNZ LOOP

     HLT
