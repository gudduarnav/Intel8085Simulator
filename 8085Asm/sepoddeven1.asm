; separate even and odd numbers from number series from 0201h,
; where N is given in 0200h. The evens and odds are stored in 0300h, 0400h.
; author Arnav Mukhopadhyay
; Feb 22, 2011
     
     LXI B, 0300H
     LXI D, 0400H
     LXI H, 0200H
     MOV A, M
     STA F000H
LOOP: INX H
     MOV A, M
     ANI 01H
     JNZ ODD
     MOV A, M
     STAX B
     INX B
     JMP CHK
ODD: MOV A, M
     STAX D
     INX D
CHK: LDA F000H
     DCR A
     STA F000H
     JNZ LOOP
     HLT

