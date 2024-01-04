#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Exception System.Linq.Error::ArgumentNull(System.String)
extern void Error_ArgumentNull_mC405D19505CA250B5C731803400D8DAF963F0DCD (void);
// 0x00000002 System.Exception System.Linq.Error::ArgumentOutOfRange(System.String)
extern void Error_ArgumentOutOfRange_mBF9354F0EDEE6E4BA57F3DA1663A91A5B61CEB9D (void);
// 0x00000003 System.Exception System.Linq.Error::MoreThanOneMatch()
extern void Error_MoreThanOneMatch_mEC48ECB89552B91EFD9669763848BA1DB97E991D (void);
// 0x00000004 System.Exception System.Linq.Error::NoElements()
extern void Error_NoElements_mE25C4D4F2FE86A32704624613D751BE305953E49 (void);
// 0x00000005 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000006 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::Select(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TResult>)
// 0x00000007 System.Func`2<TSource,System.Boolean> System.Linq.Enumerable::CombinePredicates(System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,System.Boolean>)
// 0x00000008 System.Func`2<TSource,TResult> System.Linq.Enumerable::CombineSelectors(System.Func`2<TSource,TMiddle>,System.Func`2<TMiddle,TResult>)
// 0x00000009 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::SelectMany(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TResult>>)
// 0x0000000A System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::SelectManyIterator(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TResult>>)
// 0x0000000B System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Take(System.Collections.Generic.IEnumerable`1<TSource>,System.Int32)
// 0x0000000C System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::TakeIterator(System.Collections.Generic.IEnumerable`1<TSource>,System.Int32)
// 0x0000000D System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::OrderBy(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>)
// 0x0000000E System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::OrderByDescending(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,TKey>)
// 0x0000000F System.Linq.IOrderedEnumerable`1<TSource> System.Linq.Enumerable::ThenBy(System.Linq.IOrderedEnumerable`1<TSource>,System.Func`2<TSource,TKey>)
// 0x00000010 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Distinct(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000011 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::DistinctIterator(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000012 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Union(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000013 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::UnionIterator(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000014 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::Intersect(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000015 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable::IntersectIterator(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000016 System.Boolean System.Linq.Enumerable::SequenceEqual(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000017 System.Boolean System.Linq.Enumerable::SequenceEqual(System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEnumerable`1<TSource>,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000018 TSource[] System.Linq.Enumerable::ToArray(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000019 System.Collections.Generic.List`1<TSource> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000001A TSource System.Linq.Enumerable::First(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000001B TSource System.Linq.Enumerable::FirstOrDefault(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000001C TSource System.Linq.Enumerable::Last(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x0000001D TSource System.Linq.Enumerable::SingleOrDefault(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x0000001E TSource System.Linq.Enumerable::ElementAt(System.Collections.Generic.IEnumerable`1<TSource>,System.Int32)
// 0x0000001F System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::Empty()
// 0x00000020 System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000021 System.Boolean System.Linq.Enumerable::Any(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000022 System.Int32 System.Linq.Enumerable::Count(System.Collections.Generic.IEnumerable`1<TSource>)
// 0x00000023 System.Boolean System.Linq.Enumerable::Contains(System.Collections.Generic.IEnumerable`1<TSource>,TSource)
// 0x00000024 System.Boolean System.Linq.Enumerable::Contains(System.Collections.Generic.IEnumerable`1<TSource>,TSource,System.Collections.Generic.IEqualityComparer`1<TSource>)
// 0x00000025 TAccumulate System.Linq.Enumerable::Aggregate(System.Collections.Generic.IEnumerable`1<TSource>,TAccumulate,System.Func`3<TAccumulate,TSource,TAccumulate>)
// 0x00000026 System.Int32 System.Linq.Enumerable::Sum(System.Collections.Generic.IEnumerable`1<System.Int32>)
extern void Enumerable_Sum_mCC08CB5DDD498532C9E9D72D8132EFE5B8E034FD (void);
// 0x00000027 System.Void System.Linq.Enumerable/Iterator`1::.ctor()
// 0x00000028 TSource System.Linq.Enumerable/Iterator`1::get_Current()
// 0x00000029 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/Iterator`1::Clone()
// 0x0000002A System.Void System.Linq.Enumerable/Iterator`1::Dispose()
// 0x0000002B System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/Iterator`1::GetEnumerator()
// 0x0000002C System.Boolean System.Linq.Enumerable/Iterator`1::MoveNext()
// 0x0000002D System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/Iterator`1::Select(System.Func`2<TSource,TResult>)
// 0x0000002E System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/Iterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x0000002F System.Object System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.get_Current()
// 0x00000030 System.Collections.IEnumerator System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000031 System.Void System.Linq.Enumerable/Iterator`1::System.Collections.IEnumerator.Reset()
// 0x00000032 System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x00000033 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Clone()
// 0x00000034 System.Void System.Linq.Enumerable/WhereEnumerableIterator`1::Dispose()
// 0x00000035 System.Boolean System.Linq.Enumerable/WhereEnumerableIterator`1::MoveNext()
// 0x00000036 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereEnumerableIterator`1::Select(System.Func`2<TSource,TResult>)
// 0x00000037 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereEnumerableIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x00000038 System.Void System.Linq.Enumerable/WhereArrayIterator`1::.ctor(TSource[],System.Func`2<TSource,System.Boolean>)
// 0x00000039 System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Clone()
// 0x0000003A System.Boolean System.Linq.Enumerable/WhereArrayIterator`1::MoveNext()
// 0x0000003B System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereArrayIterator`1::Select(System.Func`2<TSource,TResult>)
// 0x0000003C System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereArrayIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x0000003D System.Void System.Linq.Enumerable/WhereListIterator`1::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>)
// 0x0000003E System.Linq.Enumerable/Iterator`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Clone()
// 0x0000003F System.Boolean System.Linq.Enumerable/WhereListIterator`1::MoveNext()
// 0x00000040 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereListIterator`1::Select(System.Func`2<TSource,TResult>)
// 0x00000041 System.Collections.Generic.IEnumerable`1<TSource> System.Linq.Enumerable/WhereListIterator`1::Where(System.Func`2<TSource,System.Boolean>)
// 0x00000042 System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2::.ctor(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
// 0x00000043 System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::Clone()
// 0x00000044 System.Void System.Linq.Enumerable/WhereSelectEnumerableIterator`2::Dispose()
// 0x00000045 System.Boolean System.Linq.Enumerable/WhereSelectEnumerableIterator`2::MoveNext()
// 0x00000046 System.Collections.Generic.IEnumerable`1<TResult2> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::Select(System.Func`2<TResult,TResult2>)
// 0x00000047 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectEnumerableIterator`2::Where(System.Func`2<TResult,System.Boolean>)
// 0x00000048 System.Void System.Linq.Enumerable/WhereSelectArrayIterator`2::.ctor(TSource[],System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
// 0x00000049 System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::Clone()
// 0x0000004A System.Boolean System.Linq.Enumerable/WhereSelectArrayIterator`2::MoveNext()
// 0x0000004B System.Collections.Generic.IEnumerable`1<TResult2> System.Linq.Enumerable/WhereSelectArrayIterator`2::Select(System.Func`2<TResult,TResult2>)
// 0x0000004C System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectArrayIterator`2::Where(System.Func`2<TResult,System.Boolean>)
// 0x0000004D System.Void System.Linq.Enumerable/WhereSelectListIterator`2::.ctor(System.Collections.Generic.List`1<TSource>,System.Func`2<TSource,System.Boolean>,System.Func`2<TSource,TResult>)
// 0x0000004E System.Linq.Enumerable/Iterator`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2::Clone()
// 0x0000004F System.Boolean System.Linq.Enumerable/WhereSelectListIterator`2::MoveNext()
// 0x00000050 System.Collections.Generic.IEnumerable`1<TResult2> System.Linq.Enumerable/WhereSelectListIterator`2::Select(System.Func`2<TResult,TResult2>)
// 0x00000051 System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable/WhereSelectListIterator`2::Where(System.Func`2<TResult,System.Boolean>)
// 0x00000052 System.Void System.Linq.Enumerable/<>c__DisplayClass6_0`1::.ctor()
// 0x00000053 System.Boolean System.Linq.Enumerable/<>c__DisplayClass6_0`1::<CombinePredicates>b__0(TSource)
// 0x00000054 System.Void System.Linq.Enumerable/<>c__DisplayClass7_0`3::.ctor()
// 0x00000055 TResult System.Linq.Enumerable/<>c__DisplayClass7_0`3::<CombineSelectors>b__0(TSource)
// 0x00000056 System.Void System.Linq.Enumerable/<SelectManyIterator>d__17`2::.ctor(System.Int32)
// 0x00000057 System.Void System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.IDisposable.Dispose()
// 0x00000058 System.Boolean System.Linq.Enumerable/<SelectManyIterator>d__17`2::MoveNext()
// 0x00000059 System.Void System.Linq.Enumerable/<SelectManyIterator>d__17`2::<>m__Finally1()
// 0x0000005A System.Void System.Linq.Enumerable/<SelectManyIterator>d__17`2::<>m__Finally2()
// 0x0000005B TResult System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.Collections.Generic.IEnumerator<TResult>.get_Current()
// 0x0000005C System.Void System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.Collections.IEnumerator.Reset()
// 0x0000005D System.Object System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.Collections.IEnumerator.get_Current()
// 0x0000005E System.Collections.Generic.IEnumerator`1<TResult> System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.Collections.Generic.IEnumerable<TResult>.GetEnumerator()
// 0x0000005F System.Collections.IEnumerator System.Linq.Enumerable/<SelectManyIterator>d__17`2::System.Collections.IEnumerable.GetEnumerator()
// 0x00000060 System.Void System.Linq.Enumerable/<TakeIterator>d__25`1::.ctor(System.Int32)
// 0x00000061 System.Void System.Linq.Enumerable/<TakeIterator>d__25`1::System.IDisposable.Dispose()
// 0x00000062 System.Boolean System.Linq.Enumerable/<TakeIterator>d__25`1::MoveNext()
// 0x00000063 System.Void System.Linq.Enumerable/<TakeIterator>d__25`1::<>m__Finally1()
// 0x00000064 TSource System.Linq.Enumerable/<TakeIterator>d__25`1::System.Collections.Generic.IEnumerator<TSource>.get_Current()
// 0x00000065 System.Void System.Linq.Enumerable/<TakeIterator>d__25`1::System.Collections.IEnumerator.Reset()
// 0x00000066 System.Object System.Linq.Enumerable/<TakeIterator>d__25`1::System.Collections.IEnumerator.get_Current()
// 0x00000067 System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/<TakeIterator>d__25`1::System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
// 0x00000068 System.Collections.IEnumerator System.Linq.Enumerable/<TakeIterator>d__25`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000069 System.Void System.Linq.Enumerable/<DistinctIterator>d__68`1::.ctor(System.Int32)
// 0x0000006A System.Void System.Linq.Enumerable/<DistinctIterator>d__68`1::System.IDisposable.Dispose()
// 0x0000006B System.Boolean System.Linq.Enumerable/<DistinctIterator>d__68`1::MoveNext()
// 0x0000006C System.Void System.Linq.Enumerable/<DistinctIterator>d__68`1::<>m__Finally1()
// 0x0000006D TSource System.Linq.Enumerable/<DistinctIterator>d__68`1::System.Collections.Generic.IEnumerator<TSource>.get_Current()
// 0x0000006E System.Void System.Linq.Enumerable/<DistinctIterator>d__68`1::System.Collections.IEnumerator.Reset()
// 0x0000006F System.Object System.Linq.Enumerable/<DistinctIterator>d__68`1::System.Collections.IEnumerator.get_Current()
// 0x00000070 System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/<DistinctIterator>d__68`1::System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
// 0x00000071 System.Collections.IEnumerator System.Linq.Enumerable/<DistinctIterator>d__68`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000072 System.Void System.Linq.Enumerable/<UnionIterator>d__71`1::.ctor(System.Int32)
// 0x00000073 System.Void System.Linq.Enumerable/<UnionIterator>d__71`1::System.IDisposable.Dispose()
// 0x00000074 System.Boolean System.Linq.Enumerable/<UnionIterator>d__71`1::MoveNext()
// 0x00000075 System.Void System.Linq.Enumerable/<UnionIterator>d__71`1::<>m__Finally1()
// 0x00000076 System.Void System.Linq.Enumerable/<UnionIterator>d__71`1::<>m__Finally2()
// 0x00000077 TSource System.Linq.Enumerable/<UnionIterator>d__71`1::System.Collections.Generic.IEnumerator<TSource>.get_Current()
// 0x00000078 System.Void System.Linq.Enumerable/<UnionIterator>d__71`1::System.Collections.IEnumerator.Reset()
// 0x00000079 System.Object System.Linq.Enumerable/<UnionIterator>d__71`1::System.Collections.IEnumerator.get_Current()
// 0x0000007A System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/<UnionIterator>d__71`1::System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
// 0x0000007B System.Collections.IEnumerator System.Linq.Enumerable/<UnionIterator>d__71`1::System.Collections.IEnumerable.GetEnumerator()
// 0x0000007C System.Void System.Linq.Enumerable/<IntersectIterator>d__74`1::.ctor(System.Int32)
// 0x0000007D System.Void System.Linq.Enumerable/<IntersectIterator>d__74`1::System.IDisposable.Dispose()
// 0x0000007E System.Boolean System.Linq.Enumerable/<IntersectIterator>d__74`1::MoveNext()
// 0x0000007F System.Void System.Linq.Enumerable/<IntersectIterator>d__74`1::<>m__Finally1()
// 0x00000080 TSource System.Linq.Enumerable/<IntersectIterator>d__74`1::System.Collections.Generic.IEnumerator<TSource>.get_Current()
// 0x00000081 System.Void System.Linq.Enumerable/<IntersectIterator>d__74`1::System.Collections.IEnumerator.Reset()
// 0x00000082 System.Object System.Linq.Enumerable/<IntersectIterator>d__74`1::System.Collections.IEnumerator.get_Current()
// 0x00000083 System.Collections.Generic.IEnumerator`1<TSource> System.Linq.Enumerable/<IntersectIterator>d__74`1::System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
// 0x00000084 System.Collections.IEnumerator System.Linq.Enumerable/<IntersectIterator>d__74`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000085 System.Void System.Linq.EmptyEnumerable`1::.cctor()
// 0x00000086 System.Linq.IOrderedEnumerable`1<TElement> System.Linq.IOrderedEnumerable`1::CreateOrderedEnumerable(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x00000087 System.Void System.Linq.Set`1::.ctor(System.Collections.Generic.IEqualityComparer`1<TElement>)
// 0x00000088 System.Boolean System.Linq.Set`1::Add(TElement)
// 0x00000089 System.Boolean System.Linq.Set`1::Remove(TElement)
// 0x0000008A System.Boolean System.Linq.Set`1::Find(TElement,System.Boolean)
// 0x0000008B System.Void System.Linq.Set`1::Resize()
// 0x0000008C System.Int32 System.Linq.Set`1::InternalGetHashCode(TElement)
// 0x0000008D System.Collections.Generic.IEnumerator`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerator()
// 0x0000008E System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`1::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>)
// 0x0000008F System.Collections.IEnumerator System.Linq.OrderedEnumerable`1::System.Collections.IEnumerable.GetEnumerator()
// 0x00000090 System.Linq.IOrderedEnumerable`1<TElement> System.Linq.OrderedEnumerable`1::System.Linq.IOrderedEnumerable<TElement>.CreateOrderedEnumerable(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x00000091 System.Void System.Linq.OrderedEnumerable`1::.ctor()
// 0x00000092 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::.ctor(System.Int32)
// 0x00000093 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.IDisposable.Dispose()
// 0x00000094 System.Boolean System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::MoveNext()
// 0x00000095 TElement System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.Generic.IEnumerator<TElement>.get_Current()
// 0x00000096 System.Void System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.Reset()
// 0x00000097 System.Object System.Linq.OrderedEnumerable`1/<GetEnumerator>d__1::System.Collections.IEnumerator.get_Current()
// 0x00000098 System.Void System.Linq.OrderedEnumerable`2::.ctor(System.Collections.Generic.IEnumerable`1<TElement>,System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean)
// 0x00000099 System.Linq.EnumerableSorter`1<TElement> System.Linq.OrderedEnumerable`2::GetEnumerableSorter(System.Linq.EnumerableSorter`1<TElement>)
// 0x0000009A System.Void System.Linq.EnumerableSorter`1::ComputeKeys(TElement[],System.Int32)
// 0x0000009B System.Int32 System.Linq.EnumerableSorter`1::CompareKeys(System.Int32,System.Int32)
// 0x0000009C System.Int32[] System.Linq.EnumerableSorter`1::Sort(TElement[],System.Int32)
// 0x0000009D System.Void System.Linq.EnumerableSorter`1::QuickSort(System.Int32[],System.Int32,System.Int32)
// 0x0000009E System.Void System.Linq.EnumerableSorter`1::.ctor()
// 0x0000009F System.Void System.Linq.EnumerableSorter`2::.ctor(System.Func`2<TElement,TKey>,System.Collections.Generic.IComparer`1<TKey>,System.Boolean,System.Linq.EnumerableSorter`1<TElement>)
// 0x000000A0 System.Void System.Linq.EnumerableSorter`2::ComputeKeys(TElement[],System.Int32)
// 0x000000A1 System.Int32 System.Linq.EnumerableSorter`2::CompareKeys(System.Int32,System.Int32)
// 0x000000A2 System.Void System.Linq.Buffer`1::.ctor(System.Collections.Generic.IEnumerable`1<TElement>)
// 0x000000A3 TElement[] System.Linq.Buffer`1::ToArray()
// 0x000000A4 System.Void System.Collections.Generic.HashSet`1::.ctor()
// 0x000000A5 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEqualityComparer`1<T>)
// 0x000000A6 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEnumerable`1<T>)
// 0x000000A7 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Collections.Generic.IEnumerable`1<T>,System.Collections.Generic.IEqualityComparer`1<T>)
// 0x000000A8 System.Void System.Collections.Generic.HashSet`1::.ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
// 0x000000A9 System.Void System.Collections.Generic.HashSet`1::CopyFrom(System.Collections.Generic.HashSet`1<T>)
// 0x000000AA System.Void System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.Add(T)
// 0x000000AB System.Void System.Collections.Generic.HashSet`1::Clear()
// 0x000000AC System.Boolean System.Collections.Generic.HashSet`1::Contains(T)
// 0x000000AD System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32)
// 0x000000AE System.Boolean System.Collections.Generic.HashSet`1::Remove(T)
// 0x000000AF System.Int32 System.Collections.Generic.HashSet`1::get_Count()
// 0x000000B0 System.Boolean System.Collections.Generic.HashSet`1::System.Collections.Generic.ICollection<T>.get_IsReadOnly()
// 0x000000B1 System.Collections.Generic.HashSet`1/Enumerator<T> System.Collections.Generic.HashSet`1::GetEnumerator()
// 0x000000B2 System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.HashSet`1::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
// 0x000000B3 System.Collections.IEnumerator System.Collections.Generic.HashSet`1::System.Collections.IEnumerable.GetEnumerator()
// 0x000000B4 System.Void System.Collections.Generic.HashSet`1::GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
// 0x000000B5 System.Void System.Collections.Generic.HashSet`1::OnDeserialization(System.Object)
// 0x000000B6 System.Boolean System.Collections.Generic.HashSet`1::Add(T)
// 0x000000B7 System.Void System.Collections.Generic.HashSet`1::UnionWith(System.Collections.Generic.IEnumerable`1<T>)
// 0x000000B8 System.Void System.Collections.Generic.HashSet`1::CopyTo(T[])
// 0x000000B9 System.Void System.Collections.Generic.HashSet`1::CopyTo(T[],System.Int32,System.Int32)
// 0x000000BA System.Collections.Generic.IEqualityComparer`1<T> System.Collections.Generic.HashSet`1::get_Comparer()
// 0x000000BB System.Void System.Collections.Generic.HashSet`1::TrimExcess()
// 0x000000BC System.Void System.Collections.Generic.HashSet`1::Initialize(System.Int32)
// 0x000000BD System.Void System.Collections.Generic.HashSet`1::IncreaseCapacity()
// 0x000000BE System.Void System.Collections.Generic.HashSet`1::SetCapacity(System.Int32)
// 0x000000BF System.Boolean System.Collections.Generic.HashSet`1::AddIfNotPresent(T)
// 0x000000C0 System.Void System.Collections.Generic.HashSet`1::AddValue(System.Int32,System.Int32,T)
// 0x000000C1 System.Boolean System.Collections.Generic.HashSet`1::AreEqualityComparersEqual(System.Collections.Generic.HashSet`1<T>,System.Collections.Generic.HashSet`1<T>)
// 0x000000C2 System.Int32 System.Collections.Generic.HashSet`1::InternalGetHashCode(T)
// 0x000000C3 System.Void System.Collections.Generic.HashSet`1/Enumerator::.ctor(System.Collections.Generic.HashSet`1<T>)
// 0x000000C4 System.Void System.Collections.Generic.HashSet`1/Enumerator::Dispose()
// 0x000000C5 System.Boolean System.Collections.Generic.HashSet`1/Enumerator::MoveNext()
// 0x000000C6 T System.Collections.Generic.HashSet`1/Enumerator::get_Current()
// 0x000000C7 System.Object System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.get_Current()
// 0x000000C8 System.Void System.Collections.Generic.HashSet`1/Enumerator::System.Collections.IEnumerator.Reset()
static Il2CppMethodPointer s_methodPointers[200] = 
{
	Error_ArgumentNull_mC405D19505CA250B5C731803400D8DAF963F0DCD,
	Error_ArgumentOutOfRange_mBF9354F0EDEE6E4BA57F3DA1663A91A5B61CEB9D,
	Error_MoreThanOneMatch_mEC48ECB89552B91EFD9669763848BA1DB97E991D,
	Error_NoElements_mE25C4D4F2FE86A32704624613D751BE305953E49,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	Enumerable_Sum_mCC08CB5DDD498532C9E9D72D8132EFE5B8E034FD,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
};
static const int32_t s_InvokerIndices[200] = 
{
	6172,
	6172,
	6418,
	6418,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	6082,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
	-1,
};
static const Il2CppTokenRangePair s_rgctxIndices[64] = 
{
	{ 0x02000004, { 96, 4 } },
	{ 0x02000005, { 100, 9 } },
	{ 0x02000006, { 111, 7 } },
	{ 0x02000007, { 120, 10 } },
	{ 0x02000008, { 132, 11 } },
	{ 0x02000009, { 146, 9 } },
	{ 0x0200000A, { 158, 12 } },
	{ 0x0200000B, { 173, 1 } },
	{ 0x0200000C, { 174, 2 } },
	{ 0x0200000D, { 176, 12 } },
	{ 0x0200000E, { 188, 8 } },
	{ 0x0200000F, { 196, 11 } },
	{ 0x02000010, { 207, 12 } },
	{ 0x02000011, { 219, 12 } },
	{ 0x02000012, { 231, 2 } },
	{ 0x02000014, { 233, 8 } },
	{ 0x02000016, { 241, 3 } },
	{ 0x02000017, { 246, 5 } },
	{ 0x02000018, { 251, 7 } },
	{ 0x02000019, { 258, 3 } },
	{ 0x0200001A, { 261, 7 } },
	{ 0x0200001B, { 268, 4 } },
	{ 0x0200001C, { 272, 34 } },
	{ 0x0200001E, { 306, 2 } },
	{ 0x06000005, { 0, 10 } },
	{ 0x06000006, { 10, 10 } },
	{ 0x06000007, { 20, 5 } },
	{ 0x06000008, { 25, 5 } },
	{ 0x06000009, { 30, 1 } },
	{ 0x0600000A, { 31, 2 } },
	{ 0x0600000B, { 33, 1 } },
	{ 0x0600000C, { 34, 2 } },
	{ 0x0600000D, { 36, 2 } },
	{ 0x0600000E, { 38, 2 } },
	{ 0x0600000F, { 40, 1 } },
	{ 0x06000010, { 41, 1 } },
	{ 0x06000011, { 42, 2 } },
	{ 0x06000012, { 44, 1 } },
	{ 0x06000013, { 45, 2 } },
	{ 0x06000014, { 47, 1 } },
	{ 0x06000015, { 48, 2 } },
	{ 0x06000016, { 50, 1 } },
	{ 0x06000017, { 51, 5 } },
	{ 0x06000018, { 56, 3 } },
	{ 0x06000019, { 59, 2 } },
	{ 0x0600001A, { 61, 4 } },
	{ 0x0600001B, { 65, 4 } },
	{ 0x0600001C, { 69, 4 } },
	{ 0x0600001D, { 73, 3 } },
	{ 0x0600001E, { 76, 3 } },
	{ 0x0600001F, { 79, 1 } },
	{ 0x06000020, { 80, 1 } },
	{ 0x06000021, { 81, 3 } },
	{ 0x06000022, { 84, 2 } },
	{ 0x06000023, { 86, 2 } },
	{ 0x06000024, { 88, 5 } },
	{ 0x06000025, { 93, 3 } },
	{ 0x06000036, { 109, 2 } },
	{ 0x0600003B, { 118, 2 } },
	{ 0x06000040, { 130, 2 } },
	{ 0x06000046, { 143, 3 } },
	{ 0x0600004B, { 155, 3 } },
	{ 0x06000050, { 170, 3 } },
	{ 0x06000090, { 244, 2 } },
};
static const Il2CppRGCTXDefinition s_rgctxValues[308] = 
{
	{ (Il2CppRGCTXDataType)2, 6328 },
	{ (Il2CppRGCTXDataType)3, 24222 },
	{ (Il2CppRGCTXDataType)2, 10235 },
	{ (Il2CppRGCTXDataType)2, 9634 },
	{ (Il2CppRGCTXDataType)3, 41872 },
	{ (Il2CppRGCTXDataType)2, 7073 },
	{ (Il2CppRGCTXDataType)2, 9659 },
	{ (Il2CppRGCTXDataType)3, 41912 },
	{ (Il2CppRGCTXDataType)2, 9642 },
	{ (Il2CppRGCTXDataType)3, 41884 },
	{ (Il2CppRGCTXDataType)2, 6329 },
	{ (Il2CppRGCTXDataType)3, 24223 },
	{ (Il2CppRGCTXDataType)2, 10263 },
	{ (Il2CppRGCTXDataType)2, 9667 },
	{ (Il2CppRGCTXDataType)3, 41924 },
	{ (Il2CppRGCTXDataType)2, 7097 },
	{ (Il2CppRGCTXDataType)2, 9687 },
	{ (Il2CppRGCTXDataType)3, 42031 },
	{ (Il2CppRGCTXDataType)2, 9677 },
	{ (Il2CppRGCTXDataType)3, 41973 },
	{ (Il2CppRGCTXDataType)2, 989 },
	{ (Il2CppRGCTXDataType)3, 136 },
	{ (Il2CppRGCTXDataType)3, 137 },
	{ (Il2CppRGCTXDataType)2, 3665 },
	{ (Il2CppRGCTXDataType)3, 15528 },
	{ (Il2CppRGCTXDataType)2, 990 },
	{ (Il2CppRGCTXDataType)3, 146 },
	{ (Il2CppRGCTXDataType)3, 147 },
	{ (Il2CppRGCTXDataType)2, 3677 },
	{ (Il2CppRGCTXDataType)3, 15532 },
	{ (Il2CppRGCTXDataType)3, 46512 },
	{ (Il2CppRGCTXDataType)2, 1039 },
	{ (Il2CppRGCTXDataType)3, 335 },
	{ (Il2CppRGCTXDataType)3, 46531 },
	{ (Il2CppRGCTXDataType)2, 1047 },
	{ (Il2CppRGCTXDataType)3, 371 },
	{ (Il2CppRGCTXDataType)2, 7757 },
	{ (Il2CppRGCTXDataType)3, 32933 },
	{ (Il2CppRGCTXDataType)2, 7758 },
	{ (Il2CppRGCTXDataType)3, 32934 },
	{ (Il2CppRGCTXDataType)3, 20522 },
	{ (Il2CppRGCTXDataType)3, 46462 },
	{ (Il2CppRGCTXDataType)2, 993 },
	{ (Il2CppRGCTXDataType)3, 177 },
	{ (Il2CppRGCTXDataType)3, 46567 },
	{ (Il2CppRGCTXDataType)2, 1050 },
	{ (Il2CppRGCTXDataType)3, 394 },
	{ (Il2CppRGCTXDataType)3, 46477 },
	{ (Il2CppRGCTXDataType)2, 1025 },
	{ (Il2CppRGCTXDataType)3, 300 },
	{ (Il2CppRGCTXDataType)3, 46520 },
	{ (Il2CppRGCTXDataType)3, 14347 },
	{ (Il2CppRGCTXDataType)2, 3455 },
	{ (Il2CppRGCTXDataType)2, 4122 },
	{ (Il2CppRGCTXDataType)2, 4435 },
	{ (Il2CppRGCTXDataType)2, 4658 },
	{ (Il2CppRGCTXDataType)2, 1300 },
	{ (Il2CppRGCTXDataType)3, 2484 },
	{ (Il2CppRGCTXDataType)3, 2485 },
	{ (Il2CppRGCTXDataType)2, 7074 },
	{ (Il2CppRGCTXDataType)3, 26472 },
	{ (Il2CppRGCTXDataType)2, 5526 },
	{ (Il2CppRGCTXDataType)2, 3899 },
	{ (Il2CppRGCTXDataType)2, 4136 },
	{ (Il2CppRGCTXDataType)2, 4437 },
	{ (Il2CppRGCTXDataType)2, 5527 },
	{ (Il2CppRGCTXDataType)2, 3900 },
	{ (Il2CppRGCTXDataType)2, 4137 },
	{ (Il2CppRGCTXDataType)2, 4438 },
	{ (Il2CppRGCTXDataType)2, 5528 },
	{ (Il2CppRGCTXDataType)2, 3901 },
	{ (Il2CppRGCTXDataType)2, 4138 },
	{ (Il2CppRGCTXDataType)2, 4439 },
	{ (Il2CppRGCTXDataType)2, 4139 },
	{ (Il2CppRGCTXDataType)2, 4440 },
	{ (Il2CppRGCTXDataType)3, 15529 },
	{ (Il2CppRGCTXDataType)2, 5525 },
	{ (Il2CppRGCTXDataType)2, 4135 },
	{ (Il2CppRGCTXDataType)2, 4436 },
	{ (Il2CppRGCTXDataType)2, 2492 },
	{ (Il2CppRGCTXDataType)2, 4117 },
	{ (Il2CppRGCTXDataType)2, 4118 },
	{ (Il2CppRGCTXDataType)2, 4433 },
	{ (Il2CppRGCTXDataType)3, 15527 },
	{ (Il2CppRGCTXDataType)2, 3898 },
	{ (Il2CppRGCTXDataType)2, 4134 },
	{ (Il2CppRGCTXDataType)2, 3897 },
	{ (Il2CppRGCTXDataType)3, 46452 },
	{ (Il2CppRGCTXDataType)3, 14346 },
	{ (Il2CppRGCTXDataType)2, 3454 },
	{ (Il2CppRGCTXDataType)2, 4120 },
	{ (Il2CppRGCTXDataType)2, 4434 },
	{ (Il2CppRGCTXDataType)2, 4657 },
	{ (Il2CppRGCTXDataType)2, 4163 },
	{ (Il2CppRGCTXDataType)2, 4444 },
	{ (Il2CppRGCTXDataType)3, 15716 },
	{ (Il2CppRGCTXDataType)3, 24224 },
	{ (Il2CppRGCTXDataType)3, 24226 },
	{ (Il2CppRGCTXDataType)2, 697 },
	{ (Il2CppRGCTXDataType)3, 24225 },
	{ (Il2CppRGCTXDataType)3, 24234 },
	{ (Il2CppRGCTXDataType)2, 6332 },
	{ (Il2CppRGCTXDataType)2, 9643 },
	{ (Il2CppRGCTXDataType)3, 41885 },
	{ (Il2CppRGCTXDataType)3, 24235 },
	{ (Il2CppRGCTXDataType)2, 4225 },
	{ (Il2CppRGCTXDataType)2, 4491 },
	{ (Il2CppRGCTXDataType)3, 15539 },
	{ (Il2CppRGCTXDataType)3, 46427 },
	{ (Il2CppRGCTXDataType)2, 9678 },
	{ (Il2CppRGCTXDataType)3, 41974 },
	{ (Il2CppRGCTXDataType)3, 24227 },
	{ (Il2CppRGCTXDataType)2, 6331 },
	{ (Il2CppRGCTXDataType)2, 9635 },
	{ (Il2CppRGCTXDataType)3, 41873 },
	{ (Il2CppRGCTXDataType)3, 15538 },
	{ (Il2CppRGCTXDataType)3, 24228 },
	{ (Il2CppRGCTXDataType)3, 46426 },
	{ (Il2CppRGCTXDataType)2, 9668 },
	{ (Il2CppRGCTXDataType)3, 41925 },
	{ (Il2CppRGCTXDataType)3, 24241 },
	{ (Il2CppRGCTXDataType)2, 6333 },
	{ (Il2CppRGCTXDataType)2, 9660 },
	{ (Il2CppRGCTXDataType)3, 41913 },
	{ (Il2CppRGCTXDataType)3, 26541 },
	{ (Il2CppRGCTXDataType)3, 12510 },
	{ (Il2CppRGCTXDataType)3, 15540 },
	{ (Il2CppRGCTXDataType)3, 12509 },
	{ (Il2CppRGCTXDataType)3, 24242 },
	{ (Il2CppRGCTXDataType)3, 46428 },
	{ (Il2CppRGCTXDataType)2, 9688 },
	{ (Il2CppRGCTXDataType)3, 42032 },
	{ (Il2CppRGCTXDataType)3, 24255 },
	{ (Il2CppRGCTXDataType)2, 6335 },
	{ (Il2CppRGCTXDataType)2, 9680 },
	{ (Il2CppRGCTXDataType)3, 41976 },
	{ (Il2CppRGCTXDataType)3, 24256 },
	{ (Il2CppRGCTXDataType)2, 4228 },
	{ (Il2CppRGCTXDataType)2, 4494 },
	{ (Il2CppRGCTXDataType)3, 15544 },
	{ (Il2CppRGCTXDataType)3, 15543 },
	{ (Il2CppRGCTXDataType)2, 9645 },
	{ (Il2CppRGCTXDataType)3, 41887 },
	{ (Il2CppRGCTXDataType)3, 46434 },
	{ (Il2CppRGCTXDataType)2, 9679 },
	{ (Il2CppRGCTXDataType)3, 41975 },
	{ (Il2CppRGCTXDataType)3, 24248 },
	{ (Il2CppRGCTXDataType)2, 6334 },
	{ (Il2CppRGCTXDataType)2, 9670 },
	{ (Il2CppRGCTXDataType)3, 41927 },
	{ (Il2CppRGCTXDataType)3, 15542 },
	{ (Il2CppRGCTXDataType)3, 15541 },
	{ (Il2CppRGCTXDataType)3, 24249 },
	{ (Il2CppRGCTXDataType)2, 9644 },
	{ (Il2CppRGCTXDataType)3, 41886 },
	{ (Il2CppRGCTXDataType)3, 46433 },
	{ (Il2CppRGCTXDataType)2, 9669 },
	{ (Il2CppRGCTXDataType)3, 41926 },
	{ (Il2CppRGCTXDataType)3, 24262 },
	{ (Il2CppRGCTXDataType)2, 6336 },
	{ (Il2CppRGCTXDataType)2, 9690 },
	{ (Il2CppRGCTXDataType)3, 42034 },
	{ (Il2CppRGCTXDataType)3, 26542 },
	{ (Il2CppRGCTXDataType)3, 12512 },
	{ (Il2CppRGCTXDataType)3, 15546 },
	{ (Il2CppRGCTXDataType)3, 15545 },
	{ (Il2CppRGCTXDataType)3, 12511 },
	{ (Il2CppRGCTXDataType)3, 24263 },
	{ (Il2CppRGCTXDataType)2, 9646 },
	{ (Il2CppRGCTXDataType)3, 41888 },
	{ (Il2CppRGCTXDataType)3, 46435 },
	{ (Il2CppRGCTXDataType)2, 9689 },
	{ (Il2CppRGCTXDataType)3, 42033 },
	{ (Il2CppRGCTXDataType)3, 15535 },
	{ (Il2CppRGCTXDataType)3, 15536 },
	{ (Il2CppRGCTXDataType)3, 15547 },
	{ (Il2CppRGCTXDataType)3, 338 },
	{ (Il2CppRGCTXDataType)3, 337 },
	{ (Il2CppRGCTXDataType)2, 4214 },
	{ (Il2CppRGCTXDataType)2, 4483 },
	{ (Il2CppRGCTXDataType)3, 15537 },
	{ (Il2CppRGCTXDataType)2, 4252 },
	{ (Il2CppRGCTXDataType)2, 4523 },
	{ (Il2CppRGCTXDataType)3, 340 },
	{ (Il2CppRGCTXDataType)2, 900 },
	{ (Il2CppRGCTXDataType)2, 1040 },
	{ (Il2CppRGCTXDataType)3, 336 },
	{ (Il2CppRGCTXDataType)3, 339 },
	{ (Il2CppRGCTXDataType)3, 373 },
	{ (Il2CppRGCTXDataType)2, 4217 },
	{ (Il2CppRGCTXDataType)2, 4485 },
	{ (Il2CppRGCTXDataType)3, 375 },
	{ (Il2CppRGCTXDataType)2, 693 },
	{ (Il2CppRGCTXDataType)2, 1048 },
	{ (Il2CppRGCTXDataType)3, 372 },
	{ (Il2CppRGCTXDataType)3, 374 },
	{ (Il2CppRGCTXDataType)3, 179 },
	{ (Il2CppRGCTXDataType)2, 8871 },
	{ (Il2CppRGCTXDataType)3, 38143 },
	{ (Il2CppRGCTXDataType)2, 4208 },
	{ (Il2CppRGCTXDataType)2, 4479 },
	{ (Il2CppRGCTXDataType)3, 38144 },
	{ (Il2CppRGCTXDataType)3, 181 },
	{ (Il2CppRGCTXDataType)2, 688 },
	{ (Il2CppRGCTXDataType)2, 994 },
	{ (Il2CppRGCTXDataType)3, 178 },
	{ (Il2CppRGCTXDataType)3, 180 },
	{ (Il2CppRGCTXDataType)3, 396 },
	{ (Il2CppRGCTXDataType)3, 397 },
	{ (Il2CppRGCTXDataType)2, 8875 },
	{ (Il2CppRGCTXDataType)3, 38148 },
	{ (Il2CppRGCTXDataType)2, 4220 },
	{ (Il2CppRGCTXDataType)2, 4487 },
	{ (Il2CppRGCTXDataType)3, 38149 },
	{ (Il2CppRGCTXDataType)3, 399 },
	{ (Il2CppRGCTXDataType)2, 695 },
	{ (Il2CppRGCTXDataType)2, 1051 },
	{ (Il2CppRGCTXDataType)3, 395 },
	{ (Il2CppRGCTXDataType)3, 398 },
	{ (Il2CppRGCTXDataType)3, 302 },
	{ (Il2CppRGCTXDataType)2, 8873 },
	{ (Il2CppRGCTXDataType)3, 38145 },
	{ (Il2CppRGCTXDataType)2, 4211 },
	{ (Il2CppRGCTXDataType)2, 4481 },
	{ (Il2CppRGCTXDataType)3, 38146 },
	{ (Il2CppRGCTXDataType)3, 38147 },
	{ (Il2CppRGCTXDataType)3, 304 },
	{ (Il2CppRGCTXDataType)2, 690 },
	{ (Il2CppRGCTXDataType)2, 1026 },
	{ (Il2CppRGCTXDataType)3, 301 },
	{ (Il2CppRGCTXDataType)3, 303 },
	{ (Il2CppRGCTXDataType)2, 10279 },
	{ (Il2CppRGCTXDataType)2, 2493 },
	{ (Il2CppRGCTXDataType)3, 14385 },
	{ (Il2CppRGCTXDataType)2, 3470 },
	{ (Il2CppRGCTXDataType)2, 10702 },
	{ (Il2CppRGCTXDataType)3, 38140 },
	{ (Il2CppRGCTXDataType)3, 38141 },
	{ (Il2CppRGCTXDataType)2, 4674 },
	{ (Il2CppRGCTXDataType)3, 38142 },
	{ (Il2CppRGCTXDataType)2, 601 },
	{ (Il2CppRGCTXDataType)2, 998 },
	{ (Il2CppRGCTXDataType)3, 198 },
	{ (Il2CppRGCTXDataType)3, 32908 },
	{ (Il2CppRGCTXDataType)2, 7759 },
	{ (Il2CppRGCTXDataType)3, 32935 },
	{ (Il2CppRGCTXDataType)2, 1301 },
	{ (Il2CppRGCTXDataType)3, 2486 },
	{ (Il2CppRGCTXDataType)3, 32914 },
	{ (Il2CppRGCTXDataType)3, 12455 },
	{ (Il2CppRGCTXDataType)2, 731 },
	{ (Il2CppRGCTXDataType)3, 32909 },
	{ (Il2CppRGCTXDataType)2, 7754 },
	{ (Il2CppRGCTXDataType)3, 2941 },
	{ (Il2CppRGCTXDataType)2, 1330 },
	{ (Il2CppRGCTXDataType)2, 2695 },
	{ (Il2CppRGCTXDataType)3, 12473 },
	{ (Il2CppRGCTXDataType)3, 32910 },
	{ (Il2CppRGCTXDataType)3, 12450 },
	{ (Il2CppRGCTXDataType)3, 12451 },
	{ (Il2CppRGCTXDataType)3, 12449 },
	{ (Il2CppRGCTXDataType)3, 12452 },
	{ (Il2CppRGCTXDataType)2, 2691 },
	{ (Il2CppRGCTXDataType)2, 10331 },
	{ (Il2CppRGCTXDataType)3, 15534 },
	{ (Il2CppRGCTXDataType)3, 12454 },
	{ (Il2CppRGCTXDataType)2, 4049 },
	{ (Il2CppRGCTXDataType)3, 12453 },
	{ (Il2CppRGCTXDataType)2, 3905 },
	{ (Il2CppRGCTXDataType)2, 10272 },
	{ (Il2CppRGCTXDataType)2, 4166 },
	{ (Il2CppRGCTXDataType)2, 4446 },
	{ (Il2CppRGCTXDataType)3, 14366 },
	{ (Il2CppRGCTXDataType)2, 3464 },
	{ (Il2CppRGCTXDataType)3, 16331 },
	{ (Il2CppRGCTXDataType)3, 16332 },
	{ (Il2CppRGCTXDataType)2, 3836 },
	{ (Il2CppRGCTXDataType)3, 16335 },
	{ (Il2CppRGCTXDataType)2, 3836 },
	{ (Il2CppRGCTXDataType)3, 16336 },
	{ (Il2CppRGCTXDataType)2, 3909 },
	{ (Il2CppRGCTXDataType)3, 16340 },
	{ (Il2CppRGCTXDataType)3, 16344 },
	{ (Il2CppRGCTXDataType)3, 16343 },
	{ (Il2CppRGCTXDataType)2, 10700 },
	{ (Il2CppRGCTXDataType)3, 16334 },
	{ (Il2CppRGCTXDataType)3, 16333 },
	{ (Il2CppRGCTXDataType)3, 16341 },
	{ (Il2CppRGCTXDataType)2, 4669 },
	{ (Il2CppRGCTXDataType)3, 16338 },
	{ (Il2CppRGCTXDataType)3, 47360 },
	{ (Il2CppRGCTXDataType)2, 2699 },
	{ (Il2CppRGCTXDataType)3, 12503 },
	{ (Il2CppRGCTXDataType)1, 4042 },
	{ (Il2CppRGCTXDataType)2, 10285 },
	{ (Il2CppRGCTXDataType)3, 16337 },
	{ (Il2CppRGCTXDataType)1, 10285 },
	{ (Il2CppRGCTXDataType)1, 4669 },
	{ (Il2CppRGCTXDataType)2, 10700 },
	{ (Il2CppRGCTXDataType)2, 10285 },
	{ (Il2CppRGCTXDataType)2, 4173 },
	{ (Il2CppRGCTXDataType)2, 4451 },
	{ (Il2CppRGCTXDataType)3, 16342 },
	{ (Il2CppRGCTXDataType)3, 16339 },
	{ (Il2CppRGCTXDataType)3, 16345 },
	{ (Il2CppRGCTXDataType)2, 501 },
	{ (Il2CppRGCTXDataType)3, 12513 },
	{ (Il2CppRGCTXDataType)2, 708 },
};
extern const CustomAttributesCacheGenerator g_System_Core_AttributeGenerators[];
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_System_Core_CodeGenModule;
const Il2CppCodeGenModule g_System_Core_CodeGenModule = 
{
	"System.Core.dll",
	200,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	64,
	s_rgctxIndices,
	308,
	s_rgctxValues,
	NULL,
	g_System_Core_AttributeGenerators,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
