#if TOOLS

using Godot;

namespace BasicShmup.addons.ResourcePreview;

public partial class NodePreviewInspectorPlugin : EditorInspectorPlugin
{
    public override bool _CanHandle(GodotObject @object)
    {
        return @object is IResourcePreview;
    }

    public override void _ParseBegin(GodotObject @object)
    {
        if (@object is not IResourcePreview resourcePreview)
            return;

        var panel = new NodePreviewPanel();
        panel.SetupPreview(resourcePreview);

        AddCustomControl(panel);
    }
}

#endif
