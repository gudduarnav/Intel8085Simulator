; add 2 bcd 8bit numbers at 0200, 0201 and store result in 0202h, carry in
; 0203h
; author Arnav Mukhopadhyay
; Feb 22, 2012

     lxi h, 0200h
     xra a
     mov a, m
     inx h
     add m
     daa
     inx h
     mov m, a
     mvi a, 00h
     adc a
     inx h
     mov m, a
     hlt