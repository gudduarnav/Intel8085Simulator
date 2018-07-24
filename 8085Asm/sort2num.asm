; Compare 2 8 bit numbers in 2 consecutive memory locations, and
; store the larger number in first location while the smaller in
; second;
; Written By Arnav Mukhopadhyay
;
; Nov 08, 2011

	LHLD 0200H
	MOV A, L
	CMP H
	JP SVE
	CALL SWAPHL
SVE:	SHLD 0200H
	HLT
SWAPHL:	MOV A, H
	MOV H, L
	MOV L, A
	RET
