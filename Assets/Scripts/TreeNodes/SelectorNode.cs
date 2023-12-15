public class SelectorNode : BehaviourTreeNode
{
    public override bool process()
    {
        foreach (BehaviourTreeNode child in children)
            if (child.process())
                return true;
        return false;
    }
}