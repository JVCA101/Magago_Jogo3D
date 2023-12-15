public class SequenceNode : BehaviourTreeNode
{
    public override bool process()
    {
        foreach (BehaviourTreeNode child in children)
            if (!child.process())
                return false;
        return true;
    }
}