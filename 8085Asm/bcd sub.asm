; subtract 2 bcd 8bit numbers at 0200-0201 and store result in 0202h
; author Arnav Mukhopadhyay
; Feb 22, 2012

     lxi h, 0200h
     mvi a, 99h
     sub m
     inr a
     inx h
     add m
     daa
     inx h
     mov m, a
     hlt

