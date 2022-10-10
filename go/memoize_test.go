package main

import (
	"testing"
)

func Memoize[TIn comparable, TOut any](fn func(in TIn) TOut) func(in TIn) TOut {
	cache := map[TIn]TOut{}

	return func(in TIn) TOut {
		result, ok := cache[in]

		if !ok {
			result = fn(in)
			cache[in] = result
		}

		return result
	}
}

func TestShouldReturnCachedValue(t *testing.T) {
	invoked := 0
	doubleFunc := func(number int) int {
		invoked++
		return number * 2
	}
	memoized := Memoize(doubleFunc)

	a := memoized(2)
	b := memoized(4)
	c := memoized(2)

	if a != 4 {
		t.Fatalf("Expected a to be 4, got %v", a)
	}

	if b != 8 {
		t.Fatalf("Expected b to be 8, got %v", a)
	}

	if c != 4 {
		t.Fatalf("Expected c to be 4, got %v", a)
	}

	if invoked != 2 {
		t.Fatalf("Expected function to be invoked 2 times, got %v", invoked)
	}
}
