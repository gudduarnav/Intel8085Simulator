; Assume a 8 bit number XYH in memory location 0200h. Fetch the
; number and store 0Xh and 0Yh in the 2 memory locations.
; Written By Arnav Mukhopadhyay
;
; Nov 08, 2011

	LDA 0200H
     MOV B, A
     ANI 0FH
     MOV L, A
     MOV A, B
     ANI F0H
     RRC
     RRC
     RRC
     RRC
     MOV H, A
     SHLD 0201H
     HLT



