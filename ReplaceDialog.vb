﻿Public Class ReplaceDialog
    Private MainPad As NotePad  ' 主窗体
    Private MainContent As String  ' 主窗体的文本内容(查找用)
    Private FindContent As String  ' 要查找的文本内容
    Private ReplaceContent As String  ' 要替换的文本内容
    Private StartIndex As Integer  ' 开始查找的小标
    Private TargetIndex As Integer  ' 查找到的下标
    Private NewIndex As Integer  ' 文本替换过后的下标

    Public Sub New(mainPad As NotePad)
        Me.MainPad = mainPad
    End Sub

    ' 监听键盘的按键
    Private Sub KeysClick(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then  ' 【回车键】被按下时
            If BTN_Next.Enabled = True Then
                BTN_Next.PerformClick()  ' 调用【查找下一个】按键
            End If
        End If
    End Sub

    ' 查找内容
    Private Sub LB_FindContent_Click(sender As Object, e As EventArgs) Handles LB_FindContent.Click
        TB_FindContent.SelectAll()
    End Sub

    ' 替换为
    Private Sub LB_ReplaceContent_Click(sender As Object, e As EventArgs) Handles LB_ReplaceContent.Click
        TB_ReplaceContent.SelectAll()
    End Sub

    ' 执行查找
    Private Sub Find(
        isCaseSensitive As Boolean,  ' 区分大小写
        isLoop As Boolean  ' 循环
        )
        If isCaseSensitive Then  ' 区分大小写
            If isLoop Then  ' 循环
                Find_CaseLoop()
            Else  ' 不循环
                Find_CaseNotLoop()
            End If
        Else  ' 不区分大小写
            If isLoop Then  ' 循环
                Find_NotCaseLoop()
            Else  ' 不循环
                Find_NotCaseNotLoop()
            End If
        End If

    End Sub

    ' 区分大小写，不循环查找
    Private Sub Find_CaseNotLoop()
        Me.FindContent = TB_FindContent.Text  ' 获取要查找的文本
        Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取主窗体的文本

        Me.StartIndex = Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength  ' 获取查找的起始下标
        Me.MainContent = Me.MainContent.Substring(Me.StartIndex)  ' 获取查找的源文本
        Me.TargetIndex = Me.MainContent.IndexOf(Me.FindContent)  ' 获取查找到的目标下标

        If Me.TargetIndex < 0 Then  ' 没查找到
            MessageBox.Show("找不到" & Chr(34) & Me.FindContent & Chr(34))
        Else  ' 查找到了
            SelectText(Me.StartIndex + Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
        End If
    End Sub

    ' 区分大小写，循环查找
    Private Sub Find_CaseLoop()
        Me.FindContent = TB_FindContent.Text  ' 获取要查找的文本
        Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取主窗体的文本

        Me.StartIndex = Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength  ' 获取查找的起始下标
        Me.MainContent = Me.MainContent.Substring(Me.StartIndex)  ' 获取查找的源文本
        Me.TargetIndex = Me.MainContent.IndexOf(Me.FindContent)  ' 获取查找到的目标下标

        If Me.TargetIndex < 0 Then  ' 没查找到
            ' 从开头开始循环查找
            Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取查找的源文本
            Me.TargetIndex = Me.MainContent.IndexOf(Me.FindContent)  ' 获取查找到的目标下标
            If Me.TargetIndex < 0 Then  ' 没查找到
                MessageBox.Show("找不到" & Chr(34) & Me.FindContent & Chr(34))
            Else  ' 查找到了
                SelectText(Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
            End If
        Else  ' 查找到了
            SelectText(Me.StartIndex + Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
        End If
    End Sub

    ' 不区分大小写，不循环查找
    Private Sub Find_NotCaseNotLoop()
        Me.FindContent = TB_FindContent.Text  ' 获取要查找的文本
        Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取主窗体的文本

        Me.StartIndex = Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength  ' 获取查找的起始下标
        Me.MainContent = Me.MainContent.Substring(Me.StartIndex)  ' 获取查找的源文本
        Me.TargetIndex = Me.MainContent.ToUpper.IndexOf(Me.FindContent.ToUpper)  ' 获取查找到的目标下标(不区分大小写，所以把文本全部转换成大写)

        If Me.TargetIndex < 0 Then  ' 没查找到
            MessageBox.Show("找不到" & Chr(34) & Me.FindContent & Chr(34))
        Else  ' 查找到了
            SelectText(Me.StartIndex + Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
        End If
    End Sub

    ' 不区分大小写，循环查找
    Private Sub Find_NotCaseLoop()
        Me.FindContent = TB_FindContent.Text  ' 获取要查找的文本
        Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取主窗体的文本

        Me.StartIndex = Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength  ' 获取查找的起始下标
        Me.MainContent = Me.MainContent.Substring(Me.StartIndex)  ' 获取查找的源文本
        Me.TargetIndex = Me.MainContent.ToUpper.IndexOf(Me.FindContent.ToUpper)  ' 获取查找到的目标下标

        If Me.TargetIndex < 0 Then  ' 没查找到
            ' 从开头开始循环查找
            Me.MainContent = Me.MainPad.TB_Editor.Text  ' 获取查找的源文本
            Me.TargetIndex = Me.MainContent.ToUpper.IndexOf(Me.FindContent.ToUpper)  ' 获取查找到的目标下标
            If Me.TargetIndex < 0 Then  ' 没查找到
                MessageBox.Show("找不到" & Chr(34) & Me.FindContent & Chr(34))
            Else  ' 查找到了
                SelectText(Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
            End If
        Else  ' 查找到了
            SelectText(Me.StartIndex + Me.TargetIndex, Me.FindContent.Length)  ' 选中查找到的文本
        End If
    End Sub

    ' 选中文本
    Private Sub SelectText(start As Integer, length As Integer)
        Me.MainPad.TB_Editor.Select(start, length)
    End Sub

    ' 查找下一个
    Private Sub BTN_Next_Click(sender As Object, e As EventArgs) Handles BTN_Next.Click
        Find(CB_IsCaseSensitive.Checked, CB_IsLoop.Checked)  ' 执行查找
    End Sub

    ' 替换
    Private Sub BTN_Replace_Click(sender As Object, e As EventArgs) Handles BTN_Replace.Click
        Me.ReplaceContent = TB_ReplaceContent.Text  ' 获取替换文本

        ' 实现思路如下
        ' ①如果主窗体的文本选中内容和要查找的内容一致的话，就替换该内容，结束本方法。
        ' ②执行查找。如果能查找到符合要求的文本，就替换。如果未能查找到符合要求的文本，就结束本方法。

        Me.FindContent = TB_FindContent.Text  ' 获取查找文本

        If CB_IsCaseSensitive.Checked Then  ' 区分大小写
            If Me.MainPad.TB_Editor.SelectedText = Me.FindContent And (Not Me.FindContent = Nothing) Then
                Me.NewIndex = Me.MainPad.TB_Editor.SelectionStart + Me.ReplaceContent.Length  ' 获取替换后的新下标
                ' 替换后的文本如下
                ' 被替换文本的前面部分 + 替换文本 + 被替换文本的后面部分
                Me.MainContent = Me.MainPad.TB_Editor.Text  ' 被替换文本的前面部分
                Me.MainContent = Me.MainContent.Substring(0, Me.MainPad.TB_Editor.SelectionStart) _
                    & Me.ReplaceContent _  ' 替换文本
                    & Me.MainContent.Substring(Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength)  ' 被替换文本的后面部分
                Me.MainPad.TB_Editor.Text = Me.MainContent
                Me.MainPad.TB_Editor.SelectionStart = Me.NewIndex  ' 设置新下标
                Exit Sub  ' 结束本方法
            End If
        Else  ' 不区分大小写
            If Me.MainPad.TB_Editor.SelectedText.ToUpper = Me.FindContent.ToUpper And (Not Me.FindContent = Nothing) Then
                Me.NewIndex = Me.MainPad.TB_Editor.SelectionStart + Me.ReplaceContent.Length  ' 获取替换后的新下标
                ' 替换后的文本如下
                ' 被替换文本的前面部分 + 替换文本 + 被替换文本的后面部分
                Me.MainContent = Me.MainPad.TB_Editor.Text  ' 被替换文本的前面部分
                Me.MainContent = Me.MainContent.Substring(0, Me.MainPad.TB_Editor.SelectionStart) _
                    & Me.ReplaceContent _  ' 替换文本
                    & Me.MainContent.Substring(Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength)  ' 被替换文本的后面部分
                Me.MainPad.TB_Editor.Text = Me.MainContent
                Me.MainPad.TB_Editor.SelectionStart = Me.NewIndex  ' 设置新下标
                Exit Sub  ' 结束本方法
            End If
        End If

        Find(CB_IsCaseSensitive.Checked, CB_IsLoop.Checked)  ' 执行查找
        If Me.MainPad.TB_Editor.SelectedText = Nothing Then  ' 主窗体没有被选中的文本，也就是说没有查找到相应内容
            Exit Sub
        End If

        Me.NewIndex = Me.MainPad.TB_Editor.SelectionStart + Me.ReplaceContent.Length  ' 获取替换后的新下标
        ' 替换后的文本如下
        ' 被替换文本的前面部分 + 替换文本 + 被替换文本的后面部分
        Me.MainContent = Me.MainPad.TB_Editor.Text  ' 被替换文本的前面部分
        Me.MainContent = Me.MainContent.Substring(0, Me.MainPad.TB_Editor.SelectionStart) _
            & Me.ReplaceContent _  ' 替换文本
            & Me.MainContent.Substring(Me.MainPad.TB_Editor.SelectionStart + Me.MainPad.TB_Editor.SelectionLength)  ' 被替换文本的后面部分
        Me.MainPad.TB_Editor.Text = Me.MainContent
        Me.MainPad.TB_Editor.SelectionStart = Me.NewIndex  ' 设置新下标
    End Sub

    ' 取消
    Private Sub BTN_Cancel_Click(sender As Object, e As EventArgs) Handles BTN_Cancel.Click
        Me.Close()  ' 关闭对话框
    End Sub

    ' 【查找内容】文本框
    Private Sub TB_FindContent_TextChanged(sender As Object, e As EventArgs) Handles TB_FindContent.TextChanged
        If TB_FindContent.Text = Nothing Then  ' 【查找内容】文本框内容为空
            BTN_Next.Enabled = False  ' 【查找下一个】不可用
            BTN_Replace.Enabled = False  ' 【替换】不可用
            BTN_ReplaceAll.Enabled = False  ' 【全部替换】不可用
        Else
            BTN_Next.Enabled = True  ' 【查找下一个】可用
            BTN_Replace.Enabled = True  ' 【替换】可用
            BTN_ReplaceAll.Enabled = True  ' 【全部替换】可用
        End If
    End Sub

    ' 全部替换
    Private Sub BTN_ReplaceAll_Click(sender As Object, e As EventArgs) Handles BTN_ReplaceAll.Click
        Dim replaceCount As Integer = 0  ' 记录替换的次数
        Dim oldContent As String = ""  ' 旧的文本内容
        Dim newContent As String = ""  ' 新的文本内容
        Dim currentIndex As Integer  ' 当前下标
        Dim isLoopable As Boolean = True  ' 可循环标识符

        oldContent = Me.MainPad.TB_Editor.Text  ' 获取主窗体文本
        Me.FindContent = TB_FindContent.Text  ' 获取查找文本
        Me.ReplaceContent = TB_ReplaceContent.Text  ' 获取替换文本

        If CB_IsCaseSensitive.Checked Then  ' 区分大小写
            While currentIndex <= oldContent.Length And currentIndex + Me.FindContent.Length < oldContent.Length  ' 下标有效
                If oldContent.Substring(currentIndex, Me.FindContent.Length) = Me.FindContent Then
                    newContent &= Me.ReplaceContent  ' 编辑新的文本内容
                    replaceCount += +1  ' 替换次数自增
                    currentIndex += Me.FindContent.Length  ' 设置新的下标
                Else
                    newContent &= oldContent.Substring(currentIndex, 1)  ' 编辑新的文本内容
                    currentIndex += 1  ' 设置新的下标
                End If
            End While
        Else  ' 不区分大小写
            While currentIndex <= oldContent.Length And currentIndex + Me.FindContent.Length < oldContent.Length  ' 下标有效
                If oldContent.Substring(currentIndex, Me.FindContent.Length).ToUpper = Me.FindContent.ToUpper Then
                    newContent &= Me.ReplaceContent  ' 编辑新的文本内容
                    replaceCount += +1  ' 替换次数自增
                    currentIndex += Me.FindContent.Length  ' 设置新的下标
                Else
                    newContent &= oldContent.Substring(currentIndex, 1)  ' 编辑新的文本内容
                    currentIndex += 1  ' 设置新的下标
                End If
            End While
        End If

        If replaceCount = 0 Then
            MessageBox.Show("找不到" & Chr(34) & Me.FindContent & Chr(34))
            Exit Sub
        End If

        Me.MainPad.TB_Editor.Text = newContent  ' 更新主窗体的文本内容
        MessageBox.Show("完成" & replaceCount & "处替换。")
    End Sub

End Class
