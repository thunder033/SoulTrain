#C# Unit Tests

[To Add a Unit Test:](https://msdn.microsoft.com/en-us/library/ms182532.aspx#Anchor_3)

1. Create a new method with the tag `[TestMethod]`
2. Add code to test
3. Finish test with a method from the [`Assert`](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.unittesting.assert.aspx) class (eg. `areEqual`, `areNotEqual`, `isTrue`, etc.)

```C#
[TestMethod]
public void TestMathWorks()
{
  int a = 2 + 2;
  Assert.areEqual(a, 4);
}
```

[To Run Unit Tests](https://msdn.microsoft.com/en-us/library/ms182532.aspx#Anchor_4)

1. From the top menu, open `Test`>`Windows`>`Test Explorer`
2. The Test Explorer should apper on the right side of the screen
3. Click the `Run All` button
