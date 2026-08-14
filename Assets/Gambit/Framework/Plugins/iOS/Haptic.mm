//
//  Haptic.mm
//  framework
//
//  Created by mert on 01/01/2026.
//

#import <UIKit/UIKit.h>
#include <cstring>

static UINotificationFeedbackGenerator *notificationGenerator = nil;
static UIImpactFeedbackGenerator *lightImpactGenerator = nil;
static UIImpactFeedbackGenerator *mediumImpactGenerator = nil;
static UIImpactFeedbackGenerator *heavyImpactGenerator = nil;
static UISelectionFeedbackGenerator *selectionGenerator = nil;

static void EnsureGeneratorsInitialized(void) {
    if (notificationGenerator == nil) {
        notificationGenerator = [[UINotificationFeedbackGenerator alloc] init];
        lightImpactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight];
        mediumImpactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
        heavyImpactGenerator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleHeavy];
        selectionGenerator = [[UISelectionFeedbackGenerator alloc] init];
    }
}

void Feedback(int type) {
    EnsureGeneratorsInitialized();

    switch (type) {
        case 0: // warning
            [notificationGenerator notificationOccurred:UINotificationFeedbackTypeWarning];
            [notificationGenerator prepare];
            break;
        case 1: // failure
            [notificationGenerator notificationOccurred:UINotificationFeedbackTypeError];
            [notificationGenerator prepare];
            break;
        case 2: // success
            [notificationGenerator notificationOccurred:UINotificationFeedbackTypeSuccess];
            [notificationGenerator prepare];
            break;
        case 3: // light
            [lightImpactGenerator impactOccurred];
            [lightImpactGenerator prepare];
            break;
        case 4: // medium
            [mediumImpactGenerator impactOccurred];
            [mediumImpactGenerator prepare];
            break;
        case 5: // heavy
            [heavyImpactGenerator impactOccurred];
            [heavyImpactGenerator prepare];
            break;
        case 6: // selection
            [selectionGenerator selectionChanged];
            [selectionGenerator prepare];
            break;
        default:
            NSLog(@"Haptic: invalid type %d", type);
            break;
    }
}

extern "C" {
    void _Feedback(const char* type) {
        if (strcmp(type, "Warning") == 0) Haptic(0);
        else if (strcmp(type, "Failure") == 0) Haptic(1);
        else if (strcmp(type, "Success") == 0) Haptic(2);
        else if (strcmp(type, "Light") == 0) Haptic(3);
        else if (strcmp(type, "Medium") == 0) Haptic(4);
        else if (strcmp(type, "Heavy") == 0) Haptic(5);
        else if (strcmp(type, "Selection") == 0) Haptic(6);
        else NSLog(@"Haptic: invalid type passed to _Haptic: %s", type);
    }
}