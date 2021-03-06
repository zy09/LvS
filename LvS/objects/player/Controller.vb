﻿'Description: Controller class
'Authors: George Birbilis (birbilis@kagi.com)
'Version: 20120912

Imports System.ComponentModel
Imports LvS.models.player

Namespace LvS.objects.controllers

  Public Class Controller
    Implements IController

#Region "Fields"

    Protected fPlayer As IPlayer

#End Region

#Region "Properties"

    Public Property Player() As IPlayer Implements IController.Player
      Get
        Return fPlayer
      End Get
      Set(ByVal value As IPlayer)
        fPlayer = value
        AudioVolumeController1.AudioVolume = value
        sliderTime.TimePosition = value
        sliderTime.TimeSelection = value
        If fPlayer IsNot Nothing Then
          SubtitlesOn = fPlayer.SubtitlesOn
          FullScreen = fPlayer.FullScreen
        End If
      End Set
    End Property

    Public Property Time() As Double Implements IController.Time
      Get
        Return sliderTime.Time
      End Get
      Set(ByVal value As Double)
        sliderTime.Time = value
      End Set
    End Property

    Public Property Duration() As Double Implements IController.Duration
      Get
        Return sliderTime.Duration
      End Get
      Set(ByVal value As Double)
        sliderTime.Duration = value
      End Set
    End Property

    Property SelectionStartTime() As Double Implements IController.SelectionStartTime
      Get
        Return sliderTime.SelectionStartTime
      End Get
      Set(ByVal value As Double)
        sliderTime.SelectionStartTime = value
      End Set
    End Property

    Property SelectionEndTime() As Double Implements IController.SelectionEndTime
      Get
        Return sliderTime.SelectionEndTime
      End Get
      Set(ByVal value As Double)
        sliderTime.SelectionEndTime = value
      End Set
    End Property

    <DefaultValue(True)> _
    Public Property SpeedControlsVisible() As Boolean Implements IController.SpeedControlsVisible
      Get
        Return panelSpeedControls.Visible
      End Get
      Set(ByVal value As Boolean)
        panelSpeedControls.Visible = value
      End Set
    End Property

    <DefaultValue(True)> _
    Public Property SubtitlesOn() As Boolean Implements IController.SubtitlesOn
      Get
        Return cbSubtitlesOn.Checked
      End Get
      Set(ByVal value As Boolean)
        If value <> SubtitlesOn Then
          cbSubtitlesOn.Checked = value
          SetSubtitlesOn(value)
        End If
      End Set
    End Property

    Public Property FullScreen As Boolean Implements IController.FullScreen
      Get
        Return cbFullScreen.Checked
      End Get
      Set(value As Boolean)
        If value <> FullScreen Then
          cbFullScreen.Checked = value
          SetFullScreen(value)
        End If
      End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Play() Implements IController.Play
      If fPlayer IsNot Nothing Then
        With fPlayer
          .PlaySelectionOnly = False
          .Play()
        End With
      End If
    End Sub

    Public Sub Pause() Implements IController.Pause
      If fPlayer IsNot Nothing Then fPlayer.Pause()
    End Sub

    Public Sub FastBackwards() Implements IController.FastBackwards
      If fPlayer IsNot Nothing Then
        With fPlayer
          .PlaySelectionOnly = False
          .Rate = IPlayer.Rates.rateFastBackwards
        End With
      End If
    End Sub

    Public Sub FastForward() Implements IController.FastForward
      If fPlayer IsNot Nothing Then
        With fPlayer
          .PlaySelectionOnly = False
          .Rate = IPlayer.Rates.rateFastForward
        End With
      End If
    End Sub

    Public Sub Slower() Implements IController.Slower
      If fPlayer IsNot Nothing Then fPlayer.Slower()
    End Sub

    Public Sub Faster() Implements IController.Faster
      If fPlayer IsNot Nothing Then fPlayer.Faster()
    End Sub

    Public Sub ReplaySelection() Implements IController.ReplaySelection
      If fPlayer IsNot Nothing Then
        With fPlayer
          .PlaySelectionOnly = True
          .Play(.SelectionStartTime) 'do not set .Time=.SelectionStartTime and then use Play(), it freezes
        End With
      End If
    End Sub

    Private Sub SetSubtitlesOn(ByVal value As Boolean)
      'do not set cbSubtitlesOn.Checked here
      cbSubtitlesOn.ImageIndex = If(value, 0, 1)
      RaiseEvent SubtitlesOnChanged(Me, value)
    End Sub

    Private Sub SetFullScreen(ByVal value As Boolean)
      'do not set cbFullScreen.Checked here
      cbFullScreen.ImageIndex = If(value, 0, 1)
      RaiseEvent FullScreenChanged(Me, value)
    End Sub

#End Region

#Region "Events"

    Public Event FullScreenChanged(source As models.player.IController, fullscreen As Boolean) Implements models.player.IController.FullScreenChanged
    Public Event SubtitlesOnChanged(source As models.player.IController, subtitlesOn As Boolean) Implements models.player.IController.SubtitlesOnChanged

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
      Play()
    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
      Pause()
    End Sub

    Private Sub btnFastBackwards_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFastBackwards.Click
      FastBackwards()
    End Sub

    Private Sub btnFastForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFastForward.Click
      FastForward()
    End Sub

    Private Sub btnSlower_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlower.Click
      Slower()
    End Sub

    Private Sub btnFaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFaster.Click
      Faster()
    End Sub

    Private Sub btnReplaySelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplaySelection.Click
      ReplaySelection()
    End Sub

    Private Sub cbSubtitlesOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSubtitlesOn.CheckedChanged
      SetSubtitlesOn(cbSubtitlesOn.Checked) 'do not set the property here, it checks value againt cbSubtitlesOn.Checked and acts only on change, so would do nothing
    End Sub

    Private Sub cbFullScreen_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbFullScreen.CheckedChanged
      SetFullScreen(cbFullScreen.Checked) 'do not set the property here, it checks value againt cbFullScreen.Checked and acts only on change, so would do nothing
    End Sub

#End Region

  End Class

End Namespace
