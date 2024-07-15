# Behaviour Trees

## Description

Code-only simple and lightweight behaviour trees implementation.
On the surface, it closely resembles UE Behaviour Trees implementation, but under the hood it was written with simplicity and extendability in mind.

## Installation

Git add this repository as a submodule inside your project:  
`git submodule add https://github.com/Kmiecis/Net-BehaviourTrees`

## Examples

<details>
<summary>Cooldown</summary>
<p>

#### Cooldown example.
A tree that changes color field to a random of three options between cooldowns.

```cs
var color = Color.Black;
var root = new BT_RootNode()
    .WithTask(new BT_RandomNode()
        .WithTasks(
            new BT_CustomTask().WithOnStart(delegate { color = Color.Red; }),
            new BT_CustomTask().WithOnStart(delegate { color = Color.Green; }),
            new BT_CustomTask().WithOnStart(delegate { color = Color.Blue; })
        )
        .WithConditionals(new BT_Cooldown(2.0f))
    );
```

</p>
</details>

<details>
<summary>Wait and Limit</summary>
<p>

#### Wait and Limit example.
A tree that changes color field sequentially between three values each second and halts midway last awaiter.

```cs
var color = Color.Black;
var root = new BT_RootNode()
    .WithTask(new BT_SequenceNode()
        .WithTasks(
            new BT_CustomTask().WithOnStart(delegate { color = Color.Red; }),
            new BT_Wait(1.0f),
            new BT_CustomTask().WithOnStart(delegate { color = Color.Green; }),
            new BT_Wait(1.0f),
            new BT_CustomTask().WithOnStart(delegate { color = Color.Blue; }),
            new BT_Wait(1.0f)
        )
        .WithConditionals(new BT_Limit(2.5f))
    );
```

</p>
</details>

<details>
<summary>Custom task with context and Repeats</summary>
<p>

#### Repeats example with custom contextual task. A tree does in sequence:
1. Changes color field to a random of three options each frame for 3 seconds.
2. Changes color field sequentially between three values each second 2 times.

```cs
private class ColorContext
{
    public Color color;
}

private class ChangeColorTask : BT_ATask<ColorContext>
{
    private readonly Color _color;

    public ChangeColorTask(ColorContext context, Color color) :
        base(context)
    {
        _color = color;
    }

    protected override BT_EStatus OnUpdate()
    {
        _context.color = _color;
        return BT_EStatus.Success;
    }
}

private ColorContext _colorContext = new ColorContext();

private BT_ITask CreateBehaviourTree()
{
    return new BT_RootNode()
        .WithTask(new BT_SequenceNode()
            .WithTasks(
                new BT_RandomNode()
                    .WithTasks(
                        new ChangeColorTask(_colorContext, Color.Red),
                        new ChangeColorTask(_colorContext, Color.Green),
                        new ChangeColorTask(_colorContext, Color.Blue)
                    )
                    .WithDecorators(new BT_RepeatFor(3.0f)),
                new BT_SequenceNode()
                    .WithTasks(
                        new ChangeColorTask(_colorContext, Color.Red),
                        new BT_Wait(1.0f),
                        new ChangeColorTask(_colorContext, Color.Green),
                        new BT_Wait(1.0f),
                        new ChangeColorTask(_colorContext, Color.Blue),
                        new BT_Wait(1.0f)
                    )
                    .WithDecorators(new BT_Repeat(2))
            )
        );
}
```

</p>
</details>
