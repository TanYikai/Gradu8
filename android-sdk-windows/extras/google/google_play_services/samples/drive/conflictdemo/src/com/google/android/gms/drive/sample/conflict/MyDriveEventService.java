/**
 * Copyright 2014 Google Inc. All Rights Reserved.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
 * in compliance with the License. You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software distributed under the
 * License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.google.android.gms.drive.sample.conflict;

import com.google.android.gms.drive.events.CompletionEvent;
import com.google.android.gms.drive.events.DriveEventService;

public class MyDriveEventService extends DriveEventService {

    @Override
    public void onCompletion(CompletionEvent event) {
        if (event.getStatus() == CompletionEvent.STATUS_CONFLICT) {
            // Handle completion conflict.
            ConflictResolver conflictResolver = new ConflictResolver(event, this);
            conflictResolver.resolve();
        } else if (event.getStatus() == CompletionEvent.STATUS_FAILURE) {
            // Handle completion failure.

            // CompletionEvent is only dismissed here, in a real world application failure should
            // be handled before the event is dismissed.
            event.dismiss();
        } else if (event.getStatus() == CompletionEvent.STATUS_SUCCESS) {
            // Commit completed successfully.
            event.dismiss();
        }
    }
}
