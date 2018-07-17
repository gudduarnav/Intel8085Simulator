Partial Class MachineState
    Private Sub ExecuteCU(ByRef t As Integer)
        Dim ins As Byte = GetIR()
        Dim jp As Integer = 0
        Dim op, h, l As Byte
        op = (ins >> 6) And &H3
        h = (ins >> 3) And &H7
        l = ins And &H7
        t = 0

        If op = &H0 Then
            ExecH0(h, l, jp, t)
        ElseIf op = &H1 Then
            ExecH1(h, l, jp, t)
        ElseIf op = &H2 Then
            ExecH2(h, l, jp, t)
        ElseIf op = &H3 Then
            ExecH3(h, l, jp, t)
        End If

        If jp = 0 Then
            ThrowTrap(ins, jp, t)
        End If
        NextPC(jp)
    End Sub
    Private Sub ThrowTrap(ByVal ins As Integer, ByRef iSize As Integer, ByRef t As Integer)
        ' Do nothing
        If (t = 0) And (iSize = 0) Then
            MessageBox.Show(String.Format("INVALID INSTRUCTION {0:X2} at {1:X4}", GetMemory(GetPC()), GetPC()))
            iSize = 1
            t = 4
        End If
    End Sub


    Private Sub ExecH0(ByVal h As Byte, ByVal l As Byte, ByRef iSize As Integer, ByRef t As Integer)
        If (h = &H0) And (l = &H0) Then
            ' NOP
            iSize = 1
            t = 4
        ElseIf (h = &H4) And (l = &H0) Then
            ' RIM
            iSize = 1
            t = 4
            ReadInterruptMaskToA()
        ElseIf (h = &H6) And (l = &H0) Then
            ' SIM
            iSize = 1
            t = 4
            ReadInterruptMaskFromA()
        ElseIf ((h And &H1) = 0) And (l = &H1) Then
            ' LXI [B, D, H, SP], dble
            iSize = 3
            t = 10
            Dim rr As Byte
            rr = (h >> 1) And &H3
            Dim bu, bl, b16 As Integer
            bl = GetMemory(GetPC() + 1)
            bu = GetMemory(GetPC() + 2)
            b16 = bl Or (bu << 8)

            If rr = &H3 Then
                SetSP(b16 And &HFFFF, True)
            Else
                rr *= 2
                SetRegister(CType(rr + 1, Regs), b16 And &HFF, False)
                SetRegister(CType(rr, Regs), (b16 >> 8) And &HFF, True)
            End If

        ElseIf ((h And &H1) = 1) And (l = &H1) Then
            ' DAD [B, D, H, SP]
            iSize = 1
            t = 10
            Dim hl As Integer
            hl = GetRegister(Regs.L)
            hl = hl Or (GetRegister(Regs.H) << 8)

            Dim rp As Integer = 0
            Dim rr As Integer = (h >> 1) And &H3
            If rr = &H3 Then
                rp = GetSP()
            Else
                rr *= 2
                rp = GetRegister(CType(rr + 1, Regs))
                rp = rp Or (GetRegister(CType(rr, Regs)) << 8)
            End If
            hl += rp
            SetRegister(Regs.L, hl And &HFF, False)
            SetRegister(Regs.H, (hl >> 8) And &HFF, True)
            hl = hl >> 16
            FlagCY = hl And &H1
        ElseIf ((h And &H5) = 0) And (l = &H2) Then
            ' STAX [B, D]
            iSize = 1
            t = 7
            Dim addr8, addr16, rr, v8 As Integer
            rr = (h >> 1) And &H1
            rr *= 2
            addr8 = GetRegister(CType(rr + 1, Regs))
            addr16 = addr8
            addr8 = GetRegister(CType(rr, Regs))
            addr16 = addr16 Or (addr8 << 8)
            v8 = GetRegister(Regs.A)
            SetMemory(addr16, v8, True)
        ElseIf ((h And &H5) = 1) And (l = &H2) Then

            ' LDAX [B, D]
            iSize = 1
            t = 7

            Dim addr8, addr16, rr, v8 As Integer
            rr = (h >> 1) And &H1
            rr *= 2
            addr8 = GetRegister(CType(rr + 1, Regs))
            addr16 = addr8
            addr8 = GetRegister(CType(rr, Regs))
            addr16 = addr16 Or (addr8 << 8)
            v8 = GetMemory(addr16)
            SetRegister(Regs.A, v8, True)
        ElseIf ((h And &H6) = 4) And (l = &H2) Then

            'SHLD / LHLD addr16
            iSize = 3
            t = 16
            Dim addr16, addr8 As Integer
            addr8 = GetMemory(GetPC() + 1)
            addr16 = addr8
            addr8 = GetMemory(GetPC() + 2)
            addr16 = addr16 Or (addr8 << 8)

            If (h And &H1) = 1 Then
                ' LHLD
                SetRegister(Regs.L, GetMemory(addr16), False)
                SetRegister(Regs.H, GetMemory(addr16 + 1), True)
            Else
                ' SHLD
                SetMemory(addr16, GetRegister(Regs.L), True)
                SetMemory(addr16 + 1, GetRegister(Regs.H), True)
            End If

        ElseIf ((h And &H6) = 6) And (l = &H2) Then

            'LDA / STA addr16 
            iSize = 3
            t = 13
            Dim addr8, addr16 As Integer
            addr8 = GetMemory(GetPC() + 1)
            addr16 = addr8
            addr8 = GetMemory(GetPC() + 2)
            addr16 = addr16 Or (addr8 << 8)
            If (h And &H1) = 1 Then
                ' LDA
                SetRegister(Regs.A, GetMemory(addr16), True)
            Else
                ' STA
                SetMemory(addr16, GetRegister(Regs.A), True)
            End If

        ElseIf ((h And &H1) = 0) And (l = &H3) Then
            ' INX [B, D, H, SP]
            iSize = 1
            t = 6
            Dim rr, v16, v8 As Integer
            rr = (h >> 1) And &H3
            v16 = 0
            If rr = &H3 Then
                'SP
                v16 = GetSP()
            Else
                rr *= 2
                v8 = GetRegister(CType(rr + 1, Regs))
                v16 = v8
                v8 = GetRegister(CType(rr, Regs))
                v16 = v16 Or (v8 << 8)
            End If
            v16 += 1
            If rr = &H3 Then
                'SP
                SetSP(v16 And &HFFFF, True)
            Else
                v8 = v16 And &HFF
                SetRegister(CType(rr + 1, Regs), v8, False)
                v8 = (v16 >> 8) And &HFF
                SetRegister(CType(rr, Regs), v8, True)
            End If
        ElseIf ((h And &H1) = 1) And (l = &H3) Then

            ' DCX [B, D, H, SP]
            iSize = 1
            t = 6
            Dim rr, v16, v8 As Integer
            rr = (h >> 1) And &H3
            If rr = &H3 Then
                'SP
                v16 = GetSP()
            Else
                rr *= 2
                v8 = GetRegister(CType(rr + 1, Regs))
                v16 = v8
                v8 = GetRegister(CType(rr, Regs))
                v16 = v16 Or (v8 << 8)
            End If
            v16 -= 1

            If rr = &H3 Then
                'SP
                SetSP(v16 And &HFFFF, True)
            Else

                v8 = v16 And &HFF
                SetRegister(CType(rr + 1, Regs), v8, False)
                v8 = (v16 >> 8) And &HFF
                SetRegister(CType(rr, Regs), v8, True)
            End If

            ElseIf l = &H4 Then

                ' INR [B, C, D, E, H, L, M, A]
                iSize = 1
                t = 4
                If CType(h, Regs) = Regs.M Then
                    t = 10
                End If
                Dim v8, v4 As Integer
                v8 = GetRegister(CType(h, Regs))
                v4 = v8 And &HF
                v8 += 1
                EffectAllFlagsButCYAC(v8)
                SetRegister(CType(h, Regs), v8, True)

                v4 += 1
                FlagAC = (v4 >> 4) And &H1
            ElseIf l = &H5 Then

                ' DCR [B, C, D, E, H, L, M, A]
                iSize = 1
                t = 4
                If CType(h, Regs) = Regs.M Then
                    t = 10
                End If
                Dim v8, v4 As Integer
                v8 = GetRegister(CType(h, Regs))
                v4 = v8 And &HF
                v8 -= 1
                v4 -= 1
                FlagAC = (v4 >> 4) And &H1
                EffectAllFlagsButCYAC(v8)
                SetRegister(CType(h, Regs), v8, True)
            ElseIf l = &H6 Then

                ' MVI [ B, C, D, E, H, L, M, A], val8
                iSize = 2
                t = 7
                If CType(h, Regs) = Regs.M Then
                    t = 10
                End If
                SetRegister(CType(h, Regs), GetMemory(GetPC() + 1), True)
        ElseIf (h = &H0) And (l = &H7) Then
            ' RLC
            iSize = 1
            t = 4
            ExecRLC()
        ElseIf (h = &H1) And (l = &H7) Then
            ' RRC
            iSize = 1
            t = 4
            ExecRRC()
        ElseIf (h = &H2) And (l = &H7) Then
            ' RAL
            iSize = 1
            t = 4
            ExecRAL()
        ElseIf (h = &H3) And (l = &H7) Then
            ' RAR
            iSize = 1
            t = 4
            ExecRAR()
        ElseIf (h = &H4) And (l = &H7) Then

            ' DAA
            iSize = 1
            t = 4
            Dim v8, v4 As Integer
            v8 = GetRegister(Regs.A)
            v4 = v8 And &HF
            If (FlagAC = 1) Or (v4 > 9) Then
                v8 += &H6
            End If
            v4 = v8 >> 4
            If (FlagCY = 1) Or (v4 > 9) Then
                v8 += &H60
            End If
            FlagCY = (v8 >> 8) And &H1
            v8 = v8 And &HFF
            EffectAllFlagsButCYAC(v8)
            SetRegister(Regs.A, v8, True)
            ElseIf (h = &H5) And (l = &H7) Then

                ' CMA
                iSize = 1
                t = 4
                Dim v8 As Integer
                v8 = GetRegister(Regs.A)
                v8 = Not (v8) And &HFF
                SetRegister(Regs.A, v8, True)
            ElseIf (h = &H6) And (l = &H7) Then

                'STC
                iSize = 1
                t = 4
                FlagCY = 1
            ElseIf (h = &H7) And (l = &H7) Then

                ' CMC
                iSize = 1
                t = 4
                FlagCY = Not (FlagCY) And &H1
            End If


    End Sub
    Private Sub ExecH1(ByVal h As Byte, ByVal l As Byte, ByRef iSize As Integer, ByRef t As Integer)
        If (h = &H6) And (l = &H6) Then
            th = Nothing
            NextPC(0)
            SendEventStatus(False)
            Throw New Exception()
        Else
            iSize = 1
            t = 4
            Dim val As Byte
            val = GetRegister(CType(l, Regs))
            SetRegister(CType(h, Regs), val, True)
        End If

    End Sub
    Private Sub ExecH2(ByVal h As Byte, ByVal l As Byte, ByRef iSize As Integer, ByRef t As Integer)
        Dim va, vb, vc, vf As Integer

        If h = &H0 Then
            ' ADD [B, C, D, E, H, L, M, A]
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = va + vb
            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = (vc >> 8) And &H1
            SetRegister(Regs.A, vc And &HFF, True)
            va = va And &HF
            vb = vb And &HF
            vc = va + vb
            FlagAC = (vc >> 4) And &H1
        ElseIf h = &H1 Then
            ' ADC [B, C, D, E, H, L, M, A]
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
                 va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vf = FlagCY
            vc = va + vb + vf
            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = (vc >> 8) And &H1
            SetRegister(Regs.A, vc And &HFF, True)
            va = va And &HF
            vb = vb And &HF
            vc = va + vb + vf
            FlagAC = (vc >> 4) And &H1
        ElseIf h = &H2 Then
            'SUB 
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = va - vb
            EffectAllFlagsButCYAC(vc)
            FlagCY = (vc >> 8) And &H1
            SetRegister(Regs.A, vc, True)
            va = va And &HF
            vb = vb And &HF
            vc = va - vb
            FlagAC = (vc >> 4) And &H1
        ElseIf h = &H3 Then
            'SBB
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vf = FlagCY
            vc = va - vb - vf
            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = (vc >> 8) And &H1
            SetRegister(Regs.A, vc And &HFF, True)
            va = va And &HF
            vb = vb And &HF
            vc = va - vb - vf
            FlagAC = (vc >> 4) And &H1

        ElseIf h = &H4 Then
            'ANA
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = va And vb
            vc = vc And &HFFFF

            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = 0
            FlagAC = 1
            SetRegister(Regs.A, vc And &HFF, True)

        ElseIf h = &H5 Then
            'XRA
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = (va And Not (vb)) Or (Not (va) And vb)
            vc = vc And &HFFFF

            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = 0
            FlagAC = 0
            SetRegister(Regs.A, vc And &HFF, True)

        ElseIf h = &H6 Then
            'ORA
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If
            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = va Or vb
            vc = vc And &HFFFF

            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = 0
            FlagAC = 0
            SetRegister(Regs.A, vc And &HFF, True)
        ElseIf h = &H7 Then
            ' CMP
            iSize = 1
            t = 4
            If CType(l, Regs) = Regs.M Then
                t = 7
            End If

            va = GetRegister(Regs.A)
            vb = GetRegister(CType(l, Regs))
            vc = va - vb
            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = (vc >> 8) And &H1
            va = va And &HF
            vb = vb And &HF
            vc = va - vb
            FlagAC = (vc >> 4) And &H1

        End If


    End Sub
    Private Function VerifyCond(ByVal id As Integer) As Boolean

        If id = 0 Then
            Return (Not (FlagZ) And &H1)
        ElseIf id = 1 Then
            Return FlagZ
        ElseIf id = 2 Then
            Return (Not (FlagCY) And &H1)
        ElseIf id = 3 Then
            Return FlagCY
        ElseIf id = 4 Then
            Return (Not (FlagP) And &H1)
        ElseIf id = 5 Then
            Return FlagP
        ElseIf id = 6 Then
            Return (Not (FlagS) And &H1)
        ElseIf id = 7 Then
            Return FlagS
        End If
        Return 0
    End Function
    Private Sub ExecH3(ByVal h As Byte, ByVal l As Byte, ByRef iSize As Integer, ByRef t As Integer)
        Dim addr, vl, vh As Integer
        Dim va, vb, vc, vf As Integer

        If l = &H0 Then
            ' {"RNZ", "RZ", "RNC", "RC", "RPO", "RPE", "RP", "RM"}
            iSize = 1
            If (VerifyCond(h) And 1) = 1 Then
                t = 12
                iSize = 0
                LoadPCFromStack()
            Else
                t = 6
            End If
        ElseIf ((h And &H1) = 0) And (l = &H1) Then
            '"POP {0}"
            iSize = 1
            t = 10
            vl = 0
            vh = 0
            LoadIntFromStack(vh, vl)

            h >>= 1
            If h = 0 Then
                SetRegister(Regs.B, vh, False)
                SetRegister(Regs.C, vl, True)
            ElseIf h = 1 Then
                SetRegister(Regs.D, vh, False)
                SetRegister(Regs.E, vl, True)
            ElseIf h = 2 Then
                SetRegister(Regs.H, vh, False)
                SetRegister(Regs.L, vl, True)
            Else
                SetRegister(Regs.A, vh, True)
                SetFlagRegs(vl, True)
            End If

        ElseIf (h = &H1) And (l = &H1) Then
            ' "RET"
            iSize = 0
            t = 10
            LoadPCFromStack()

        ElseIf (h = &H5) And (l = &H1) Then
            '"PCHL"
            iSize = 0
            t = 6
            vl = GetRegister(Regs.L)
            vh = GetRegister(Regs.H)
            addr = vl Or (vh << 8)
            SetPC(addr, True)

        ElseIf (h = &H7) And (l = &H1) Then
            '   "SPHL"
            iSize = 1
            t = 6
            vl = GetRegister(Regs.L)
            vh = GetRegister(Regs.H)
            addr = vl Or (vh << 8)
            SetSP(addr, True)

        ElseIf l = &H2 Then
            '{"JNZ", "JZ", "JNC", "JC", "JPO", "JPE", "JP", "JM"}
            iSize = 3
            If (VerifyCond(h) And 1) = 1 Then
                t = 10
                vl = GetMemory(GetPC() + 1)
                vh = GetMemory(GetPC() + 2)
                addr = vl Or (vh << 8)
                iSize = 0
                SetPC(addr, True)
            Else
                t = 7
            End If

        ElseIf (h = &H0) And (l = &H3) Then
            ' JMP
            iSize = 3
            t = 10
            vl = GetMemory(GetPC() + 1)
            vh = GetMemory(GetPC() + 2)
            addr = vl Or (vh << 8)
            iSize = 0
            SetPC(addr, True)
        ElseIf (h = &H2) And (l = &H3) Then
            '"OUT "
            iSize = 2
            t = 10
            vl = GetMemory(GetPC() + 1)
            OutPort(vl, True)
        ElseIf (h = &H3) And (l = &H3) Then
            'IN
            iSize = 2
            t = 10
            vl = GetMemory(GetPC() + 1)
            InPort(vl, True)
        ElseIf (h = &H5) And (l = &H3) Then
            'XCHG
            iSize = 1
            t = 4
            va = GetRegister(Regs.L)
            vb = GetRegister(Regs.E)
            Swap(va, vb)
            SetRegister(Regs.L, va, False)
            SetRegister(Regs.E, vb, False)

            va = GetRegister(Regs.H)
            vb = GetRegister(Regs.D)
            Swap(va, vb)
            SetRegister(Regs.H, va, False)
            SetRegister(Regs.D, vb, True)
        ElseIf (h = &H4) And (l = &H3) Then
            '"XTHL"
            iSize = 1
            t = 16

            va = (GetRegister(Regs.H) << 8) Or (GetRegister(Regs.L))
            vb = GetSP()
            Swap(va, vb)
            SetRegister(Regs.L, va And &HFF, False)
            SetRegister(Regs.H, (va >> 8) And &HFF, True)
            SetPC(vb And &HFFFF, True)

        ElseIf (h = &H6) And (l = &H3) Then
            '"DI"
            iSize = 1
            t = 4
            IsInterruptEnabled = False
        ElseIf (h = &H7) And (l = &H3) Then
            '"EI"
            iSize = 1
            t = 4
            IsInterruptEnabled = True
        ElseIf (l = &H4) Then
            '{"CNZ", "CZ", "CNC", "CC", "CPO", "CPE", "CP", "CM"}
            iSize = 3
            If (VerifyCond(h) And 1) = 1 Then
                t = 18
                iSize = 0
                SavePCToStack(3)
                vl = GetMemory(GetPC() + 1)
                vh = GetMemory(GetPC() + 2)
                SetPC((vh << 8) Or vl, True)

            Else
                t = 9
            End If
        ElseIf ((h And &H1) = 0) And (l = &H5) Then
            ' PUSH B, D, H, PSW
            iSize = 1
            t = 12
            h >>= 1
            If h = 0 Then
                SaveIntToStack(GetRegister(Regs.B), GetRegister(Regs.C))
            ElseIf h = 1 Then
                SaveIntToStack(GetRegister(Regs.D), GetRegister(Regs.E))
            ElseIf h = 2 Then
                SaveIntToStack(GetRegister(Regs.H), GetRegister(Regs.L))
            Else
                SaveIntToStack(GetRegister(Regs.A), GetFlagRegs())
            End If
        ElseIf (h = &H1) And (l = &H5) Then
            ' CALL addr16
            iSize = 0
            t = 18
            SavePCToStack(3)
            vl = GetMemory(GetPC() + 1)
            vh = GetMemory(GetPC() + 2)
            addr = (vh << 8) Or vl
            SetPC(addr, True)

        ElseIf (h = &H0) And (l = &H6) Then
            ' ADI byte
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = va + vb
            FlagCY = (vc >> 8) And &H1
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            va = va And &HF
            vb = vb And &HF
            vc = va + vb
            FlagAC = (vc >> 4) And &H1

        ElseIf (h = &H1) And (l = &H6) Then
            ' ACI byte
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vf = GetFlagRegs()
            vc = va + vb + vf
            FlagCY = (vc >> 8) And &H1
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            va = va And &HF
            vb = vb And &HF
            vc = va + vb + vf
            FlagAC = (vc >> 4) And &H1

        ElseIf (h = &H2) And (l = &H6) Then
            'SUI
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = va - vb
            FlagCY = (vc >> 8) And &H1
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            va = va And &HF
            vb = vb And &HF
            vc = va - vb
            FlagAC = (vc >> 4) And &H1

        ElseIf (h = &H3) And (l = &H6) Then
            'SBI 
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vf = GetFlagRegs()
            vc = va - vb - vf
            FlagCY = (vc >> 8) And &H1
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            va = va And &HF
            vb = vb And &HF
            vc = va - vb - vf
            FlagAC = (vc >> 4) And &H1

        ElseIf (h = &H4) And (l = &H6) Then
            ' ANI
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = va And vb
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            FlagCY = 0
            FlagAC = 1
        ElseIf (h = &H5) And (l = &H6) Then
            'XRI 
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = (va And Not (vb)) Or (Not (va) And vb)
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            FlagCY = 0
            FlagAC = 0

        ElseIf (h = &H6) And (l = &H6) Then
            'ORI
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = va Or vb
            vc = vc And &HFF
            SetRegister(Regs.A, vc, True)
            EffectAllFlagsButCYAC(vc)
            FlagCY = 0
            FlagAC = 0

        ElseIf (h = &H7) And (l = &H6) Then
            'CPI
            iSize = 2
            t = 7
            va = GetRegister(Regs.A)
            vb = GetMemory(GetPC() + 1)
            vc = va - vb
            EffectAllFlagsButCYAC(vc And &HFF)
            FlagCY = (vc >> 8) And &H1
            va = va And &HF
            vb = vb And &HF
            vc = va - vb
            FlagAC = (vc >> 4) And &H1

        ElseIf l = &H7 Then
            'RST
            iSize = 1
            t = 12
            Interrupt_RST(h)
        End If

    End Sub

    Private Sub EffectAllFlagsButCYAC(ByVal v8 As Integer)
        v8 = v8 And &HFF
        If v8 = 0 Then
            FlagZ = 1
        Else
            FlagZ = 0
        End If

        FlagS = (v8 >> 7) And &H1

        Dim bP, b1 As Integer
        bP = v8 And &H1
        v8 >>= 1
        While Not (v8 = 0)
            b1 = v8 And &H1
            v8 >>= 1
            bP = (b1 And Not (bP)) Or (Not (b1) And bP)
        End While
        FlagP = (Not (FlagZ) And bP) And &H1

    End Sub


    Private Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim c As Integer
        c = a
        a = b
        b = c
    End Sub

    Private Sub ExecRAL()
        Dim a, cy As Integer
        a = GetRegister(Regs.A)
        cy = FlagCY

        a = a << 1
        a = a Or cy
        cy = (a >> 8) And &H1
        a = a And &HFF

        SetRegister(Regs.A, a, True)
        FlagCY = cy
    End Sub

    Private Sub ExecRAR()
        Dim a, cy As Integer
        a = GetRegister(Regs.A)
        cy = FlagCY

        a = a Or (cy << 8)
        cy = a And &H1
        a = a >> 1
        a = a And &HFF

        SetRegister(Regs.A, a, True)
        FlagCY = cy
    End Sub

    Private Sub ExecRLC()
        Dim a, cy As Integer
        a = GetRegister(Regs.A)

        a = a << 1
        cy = (a >> 8) And &H1
        a = a Or cy
        a = a And &HFF

        SetRegister(Regs.A, a, True)
        FlagCY = cy

    End Sub

    Private Sub ExecRRC()
        Dim a, cy As Integer
        a = GetRegister(Regs.A)

        cy = a And &H1
        a = a Or (cy << 8)
        a = a >> 1
        a = a And &HFF

        SetRegister(Regs.A, a, True)
        FlagCY = cy

    End Sub


    Private Sub Save8bitToStack(ByVal d8 As Integer)
        Dim addr As Integer = GetSP()
        addr -= 1
        SetMemory(addr, d8, True)
        SetSP(addr, True)
    End Sub

    Private Function Load8bitFromStack()
        Dim addr As Integer = GetSP()
        Dim d8 As Integer
        d8 = GetMemory(addr)
        addr += 1
        SetSP(addr, True)
        Return d8
    End Function

    Private Sub SavePCToStack(ByVal ref As Integer)
        Dim addr As Integer = GetPC()
        addr += ref
        SaveIntToStack(addr >> 8, addr)
    End Sub

    Private Sub LoadPCFromStack()
        Dim addr, vl, vh As Integer
        LoadIntFromStack(vh, vl)
        addr = vl Or (vh << 8)
        SetPC(addr, True)
    End Sub

    Private Sub SaveIntToStack(ByVal vh As Integer, ByVal vl As Integer)
        Save8bitToStack(vh)
        Save8bitToStack(vl)
    End Sub

    Private Sub LoadIntFromStack(ByRef vh As Integer, ByRef vl As Integer)
        vl = Load8bitFromStack()
        vh = Load8bitFromStack()
    End Sub
End Class
