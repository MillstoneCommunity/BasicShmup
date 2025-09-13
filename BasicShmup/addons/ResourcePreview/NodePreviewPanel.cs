#if TOOLS

using Godot;

namespace BasicShmup.addons.ResourcePreview;

public partial class NodePreviewPanel : CenterContainer
{
    private SubViewport _viewport;
    private Node? _preview;

    public NodePreviewPanel()
    {
        var camera = new Camera2D();

        _viewport = new SubViewport
        {
            Size = new Vector2I(255, 255),
            Disable3D = false
        };
        _viewport.AddChild(camera);

        var viewportContainer = new SubViewportContainer
        {
            SizeFlagsVertical = SizeFlags.ExpandFill,
            SizeFlagsHorizontal = SizeFlags.ExpandFill
        };
        viewportContainer.AddChild(_viewport);

        AddChild(viewportContainer);
    }

    public void SetupPreview(IResourcePreview resourcePreview)
    {
        _preview?.QueueFree();
        _preview = resourcePreview.GetPreviewNode();

        _viewport.AddChild(_preview);
    }
}

#endif
