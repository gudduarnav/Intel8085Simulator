; Place 2 numbers in 2 consecutive memory locations 0200h and 0201h,
; where (0201h)>(0200h). Loop accumulator A from (0200h) to (0201h).
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011

	LHLD 0200H
	XRA A
	MOV A, L
RPT:	STA 0204H
     INR A
     NOP
	CMP H
     JNZ RPT
	HLT
