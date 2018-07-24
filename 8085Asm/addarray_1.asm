; 10 numbers are stored from memory location 0200h, add them and store the
; result in then next memory location.
; author Arnav Mukhopadhyay
; Feb 1, 2011

     LXI H, 0200H
     MVI B, 0AH
     XRA A
RPT: ADD M
     INX H
     DCR B
     JNZ RPT
     MOV M, A
     HLT