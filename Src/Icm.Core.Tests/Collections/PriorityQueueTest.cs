
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using Icm.Collections.Generic;

[TestFixture(), Category("Icm")]
public class PriorityQueueTest
{

	[Test()]
	public void PriorityQueue_GeneralTest()
	{
		PriorityQueue<int> miQ = new PriorityQueue<int>(maxprio: 3);
		//Enqueue with priority 0
		miQ.Enqueue(7);
		miQ.Enqueue(8);
		miQ.Enqueue(10);
		//Enqueue with priority 2
		int prio = 2;
		miQ.Enqueue(prio, 25);
		miQ.Enqueue(prio, 90);
		//Enqueue with priority 1
		prio = 1;
		miQ.Enqueue(prio, 56);
		miQ.Enqueue(prio, 21);

		Assert.AreEqual(miQ.Count, 7);

		//Dequeue
		int result = 0;

		result = miQ.Dequeue();
		Assert.AreEqual(result, 25);
		Assert.AreEqual(miQ.Count, 6);

		result = miQ.Dequeue();
		Assert.AreEqual(result, 90);
		Assert.AreEqual(miQ.Count, 5);

		result = miQ.Dequeue();
		Assert.AreEqual(result, 56);
		Assert.AreEqual(miQ.Count, 4);

		result = miQ.Dequeue();
		Assert.AreEqual(result, 21);
		Assert.AreEqual(miQ.Count, 3);


		miQ.Clear();

		Assert.AreEqual(miQ.Count, 0);

		//Dequeue with empty queue
		Assert.That(() => miQ.Dequeue(), Throws.InvalidOperationException);
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
