; check a number at 0200h and place in 0201h, the value 1 if 0200h=nonzero
; or 0, otherwise
; author Arnav Mukhopadhyay
; feb 1 2012

     LDA 0200H
     ANI FFH
     JNZ NOTZ
     MVI A, 00H
     JMP SVE
NOTZ: MVI A, 01H
SVE: STA 0201H
     HLT
    