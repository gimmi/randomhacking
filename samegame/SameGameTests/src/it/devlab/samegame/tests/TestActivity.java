package it.devlab.samegame.tests;

import android.app.Activity;
import android.os.Bundle;
import android.view.Window;

public class TestActivity extends Activity{
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.test);
	}
}
