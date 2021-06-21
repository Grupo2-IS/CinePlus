import { Router } from "@angular/router";
import { SeatsService } from "./seats.service";
import { ShowingService } from "./showing-model.service";

export function appInitializer(showingService: ShowingService, seatsService: SeatsService,
    router: Router) {

    return new Promise<void>((resolve, reject) => {
        showingService.GetActiveShowingSubs();
        showingService.GetShowingSubs();
        // seatsService.GetSeatsSub();
        resolve();
    });
}