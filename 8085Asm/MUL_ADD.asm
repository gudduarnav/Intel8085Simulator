; multiplication of 2 8 bit number by addition method
; author Arnav Mukhopadhyay
; jan 24 2012

   LHLD 0200H
   XRA A
MUL: ADD H
     DCR L
     JP MUL
     SUB H
   STA 0202H
   HLT  