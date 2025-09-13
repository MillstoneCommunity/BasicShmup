#if TOOLS
using Godot;

namespace BasicShmup.addons.ResourcePreview;

[Tool]
public partial class NodePreviewPlugin : EditorPlugin
{
    private NodePreviewInspectorPlugin? _inspectorPlugin;

    public override void _EnterTree()
    {
        GD.Print($"{nameof(NodePreviewPlugin)} loaded");

        _inspectorPlugin = new NodePreviewInspectorPlugin();
        AddInspectorPlugin(_inspectorPlugin);
    }

    public override void _ExitTree()
    {
        if (_inspectorPlugin != null)
            RemoveInspectorPlugin(_inspectorPlugin);
    }
}

#endif
