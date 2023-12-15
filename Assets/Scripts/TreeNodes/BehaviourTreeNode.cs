using System.Collections.Generic;

public abstract class BehaviourTreeNode
{
    protected List<BehaviourTreeNode> children;
    public abstract bool process();

    public BehaviourTreeNode()
    {
        children = new List<BehaviourTreeNode>();
    }

    public void addChild(BehaviourTreeNode child)
    {
        children.Add(child);
    }
}