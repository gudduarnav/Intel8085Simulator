     LHLD E100H
     XCHG
     MOV A, D
     MVI D, 00H
     LXI H, 0000H
     MVI B, 08H

NXT: RAR
     JNC NOADD
     DAD D

NOADD: XCHG
     DAD H
     XCHG

     DCR B
     JNZ NXT

     SHLD E102H
     HLT
