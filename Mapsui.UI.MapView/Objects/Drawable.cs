﻿using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.UI.Render;
using Mapsui.UI.Render.Extensions;
using Color = Mapsui.Styles.Color;


namespace Mapsui.UI.Objects;

/// <summary>
/// Base class for all drawables like polyline, polygon and circle
/// </summary>
public class Drawable : IClickable, IFeatureProvider, INotifyPropertyChanged
{
    private string? _label;
    private float _strokeWidth = 1f;
    private double _minVisible;
    private double _maxVisible = double.MaxValue;
    private int _zIndex;
    private bool _isClickable;
    private Color _strokeColor = Color.Black;
    private object? _tag;

    /// <summary>
    /// Label of drawable
    /// </summary>
    public string? Label
    {
        get => _label;
        set
        {
            if (value == _label) return;
            _label = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// StrokeWidth of drawable in pixel
    /// </summary>
    public float StrokeWidth
    {
        get => _strokeWidth;
        set
        {
            if (value.Equals(_strokeWidth)) return;
            _strokeWidth = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// StrokeColor for drawable
    /// </summary>
    public Color StrokeColor
    {
        get => _strokeColor;
        set
        {
            if (value.Equals(_strokeColor)) return;
            _strokeColor = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// MinVisible for drawable in resolution of Mapsui (smaller values are higher zoom levels)
    /// </summary>
    public double MinVisible
    {
        get => _minVisible;
        set
        {
            if (value.Equals(_minVisible)) return;
            _minVisible = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// MaxVisible for drawable in resolution of Mapsui (smaller values are higher zoom levels)
    /// </summary>
    public double MaxVisible
    {
        get => _maxVisible;
        set
        {
            if (value.Equals(_maxVisible)) return;
            _maxVisible = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// ZIndex of this drawable
    /// </summary>
    public int ZIndex
    {
        get => _zIndex;
        set
        {
            if (value == _zIndex) return;
            _zIndex = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Is this drawable clickable
    /// </summary>
    public bool IsClickable
    {
        get => _isClickable;
        set
        {
            if (value == _isClickable) return;
            _isClickable = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Object for free use
    /// </summary>
    public object? Tag
    {
        get => _tag;
        set
        {
            if (Equals(value, _tag)) return;
            _tag = value;
            OnPropertyChanged();
        }
    }

    private GeometryFeature? feature;

    /// <summary>
    /// Mapsui Feature belonging to this drawable
    /// </summary>
    public GeometryFeature? Feature
    {
        get => feature;
        set
        {
            if (feature == null || !feature.Equals(value))
            {
                if (Equals(value, feature)) return;
                feature = value;
                OnPropertyChanged();
            }
        }
    }

    GeometryFeature? IFeatureProvider.Feature => throw new NotImplementedException();


    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<DrawableClickedEventArgs> Clicked;

    

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        var vectorStyle = ((VectorStyle?)Feature?.Styles.FirstOrDefault());
        if (vectorStyle == null || vectorStyle.Line == null)
            return;

        switch (propertyName)
        {
            case nameof(StrokeWidth):
                vectorStyle.Line.Width = StrokeWidth;
                break;
            case nameof(StrokeColor):
                vectorStyle.Line.Color = StrokeColor;
                break;
            case nameof(MinVisible):
                vectorStyle.MinVisible = MinVisible;
                break;
            case nameof(MaxVisible):
                vectorStyle.MaxVisible = MaxVisible;
                break;
        }
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public void HandleClicked(DrawableClickedEventArgs e)
    {
        Clicked?.Invoke(this, e);
    }

    
}
